#region (c) Kamuoliai 2022

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using opp_library;

namespace opp_client
{
    // Main form shown for user
    public partial class ClientWindow : Form
    {
        private static HubConnection? connection;   // SignalR connection used to send requests and get responses
        private string playerID;    // current user ID, related to Player object in GameState object of server
        private PlayerInput playerInput;
        private Dictionary<string, OvalPictureBox> playerSprites;
        private OvalPictureBox ballSprite;
        private bool ballSpriteSet = false;
        private int fieldBottomOffset = 165;    // offset of playing field area from the bottom

        public ClientWindow()
        {
            InitializeComponent();
            // change 'localhost' to 'IPv4 IP' of server for local multiplayer
            connection = new HubConnectionBuilder().WithUrl("https://localhost:44330/gamehub").Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            this.playerID = String.Empty;
            this.playerInput = new PlayerInput();
            this.playerSprites = new Dictionary<string, OvalPictureBox>();

            this.FieldPictureBox.Size = new Size(ClientSize.Width, this.Height - fieldBottomOffset);
            this.FieldPictureBox.Image = Properties.Resources.BackgroundImage;
        }

        private async void ClientWindow_Load(object sender, EventArgs e)
        {
            // JoinTeamRequest response
            connection.On<string, string>("JoinTeamResponse", (newPlayerID, colorName) =>
            {
                this.playerID = newPlayerID;
                debugConsole.Items.Add($"Player with ID: '{newPlayerID}' added to {Color.FromName(colorName)} team.");
            });

            // GameStateRequest response
            connection.On<string>("GameStateResponse", (gameStateJSON) =>
            {
                GameState gameState = JsonConvert.DeserializeObject<GameState>(gameStateJSON);

                foreach (var team in gameState.Teams)
                {
                    foreach (KeyValuePair<string, Player> pair in team.Players)
                    {
                        if (!playerSprites.ContainsKey(pair.Key))
                        {
                            OvalPictureBox sprite = new OvalPictureBox();
                            sprite.Size = new Size(pair.Value.Radius, pair.Value.Radius);
                            sprite.BackColor = team.Color;
                            if (pair.Key.Equals(playerID))
                            {
                                sprite.BackColor = ControlPaint.LightLight(sprite.BackColor);
                            }

                            playerSprites.Add(pair.Key, sprite);
                            //sprite.BringToFront();
                            this.Controls.Add(sprite);
                            sprite.BringToFront();
                        }

                        playerSprites[pair.Key].Location =
                            new Point((int)pair.Value.Position.X, (int)pair.Value.Position.Y);
                    }
                }

                if (!ballSpriteSet)
                {
                    ballSprite = new OvalPictureBox();
                    ballSprite.Size = new Size(gameState.Ball.Radius, gameState.Ball.Radius);
                    ballSprite.BackColor = Color.White;
                    this.Controls.Add(ballSprite);
                    ballSprite.BringToFront();
                    ballSpriteSet = true;
                }

                ballSprite.Location = new Point((int)gameState.Ball.Position.X, (int)gameState.Ball.Position.Y);
            });

            // UpdatePlayerPositionRequest response
            connection.On<string>("UpdatePlayerPositionResponse",
                (newPosition) => { debugConsole.Items.Add($"New player position is {newPosition}"); });

            // DeleteSpriteRequest response
            connection.On<string>("DeleteSpriteResponse", (oldPlayerID) =>
            {
                this.Controls.Remove(playerSprites[oldPlayerID]);
                playerSprites.Remove(oldPlayerID);
            });

            // UpdateWindowSizeRequest response
            connection.On<int, int>("UpdateWindowSizeResponse",
                (width, height) => { this.Size = new Size(width, height); });

            try
            {
                await connection.StartAsync()!;
                debugConsole.Items.Add("Connection successful");
                // if any requests are needed when client launches, they should be put here
                await connection.InvokeAsync("UpdateFieldSizeRequest", ClientSize.Width,
                    this.Height - fieldBottomOffset);
                await connection.InvokeAsync("UpdateWindowSizeRequest", this.Width, this.Height);
                await connection.InvokeAsync("UpdateBallPositionRequest");
                await connection.InvokeAsync("GameStateRequest");
            }
            catch (Exception ex)
            {
                debugConsole.Items.Add(ex.Message);
            }
        }

        // Detects when key is pressed
        private void ClientWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                playerInput.Up = true;
            if (e.KeyCode == Keys.S)
                playerInput.Down = true;
            if (e.KeyCode == Keys.A)
                playerInput.Left = true;
            if (e.KeyCode == Keys.D)
                playerInput.Right = true;

            if (e.KeyCode == Keys.K)
                playerInput.Kick = true;
        }

        // Detects when key is no longer pressed
        private void ClientWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                playerInput.Up = false;
            if (e.KeyCode == Keys.S)
                playerInput.Down = false;
            if (e.KeyCode == Keys.A)
                playerInput.Left = false;
            if (e.KeyCode == Keys.D)
                playerInput.Right = false;

            if (e.KeyCode == Keys.K)
                playerInput.Kick = false;
        }

        private void JoinRedButton_Click(object sender, EventArgs e)
        {
            connection.InvokeAsync("JoinTeamRequest", playerID, "Red");
        }

        private void JoinBlueButton_Click(object sender, EventArgs e)
        {
            connection.InvokeAsync("JoinTeamRequest", playerID, "Blue");
        }

        // Main game loop in client
        private void GameLoop_Tick(object sender, EventArgs e)
        {
            connection.InvokeAsync("GameStateRequest");
            if (!playerID.Equals(String.Empty))
            {
                // move player if W/A/S/D keys are pressed
                if (playerInput.IsActive())
                {
                    connection.InvokeAsync("UpdatePlayerPositionRequest", playerID, playerInput);
                }

                // kick ball if K key is pressed
                if (playerInput.Kick)
                {
                    connection.InvokeAsync("KickBallRequest", playerID);
                    playerInput.Kick = false;
                }
            }
        }

        // If any client resizes their window, update window size and playing field size on other clients
        private void ClientWindow_Resize(object sender, EventArgs e)
        {
            this.FieldPictureBox.Size = new Size(ClientSize.Width, this.Height - fieldBottomOffset);
            connection.InvokeAsync("UpdateFieldSizeRequest", ClientSize.Width, this.Height - fieldBottomOffset);
            connection.InvokeAsync("UpdateWindowSizeRequest", this.Width, this.Height);
        }
    }
}
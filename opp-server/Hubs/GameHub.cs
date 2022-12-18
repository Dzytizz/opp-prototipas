#region (c) Kamuoliai 2022

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using opp_library;

namespace opp_server.Hubs
{
    // Main class for requests and responses
    public class GameHub : Hub
    {
        private GameState _gameState;
        private BallMovement _ballMovement;
        private Field _field;

        public GameHub(GameState gameState, BallMovement ballMovement, Field field)
        {
            this._gameState = gameState;
            this._ballMovement = ballMovement;
            this._ballMovement.Ball = _gameState.Ball;
            this._ballMovement.Field = field;
            this._field = field;
        }

        // Updates window size when client window is resized
        public async Task UpdateWindowSizeRequest(int width, int height)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("UpdateWindowSizeResponse", width, height);
        }

        // Updates playing field when client window is resized
        public async Task UpdateFieldSizeRequest(int width, int height)
        {
            this._field.Width = width;
            this._field.Height = height;

            Ball ball = _gameState.Ball;
            ball.Position = new Vector2(width / 2 - ball.Radius / 2, height / 2 - ball.Radius / 2);

            //await Clients.All.SendAsync("UpdateFieldSizeResponse", _field);
        }

        // Updates player position when W/A/S/D is pressed
        public async Task UpdatePlayerPositionRequest(string playerID, PlayerInput playerInput)
        {
            Player player = _gameState.GetPlayerById(playerID);
            if (player != null)
            {
                player.Move(playerInput, _field);
            }

            //await Clients.Clients(Context.ConnectionId).SendAsync("UpdatePlayerPositionResponse", player.Position.ToString());
        }

        // Moves ball to middle of playing field when client window is resized
        public async Task UpdateBallPositionRequest()
        {
            Ball ball = _gameState.Ball;
            ball.Position = new Vector2(_field.Width / 2 - ball.Radius / 2, _field.Height / 2 - ball.Radius / 2);

            //await Clients.Clients(Context.ConnectionId).SendAsync("UpdateBallPositionResponse");
        }

        // Main object with data that is constantly sent between server-client
        public async Task GameStateRequest()
        {
            GameState gameStateClone = _gameState.Clone();
            string gameStateJSON = JsonConvert.SerializeObject(gameStateClone);

            await Clients.Clients(Context.ConnectionId).SendAsync("GameStateResponse", gameStateJSON);
        }

        // Updates ball position after player pressed K key to kick
        public async Task KickBallRequest(string playerID)
        {
            Player player = _gameState.GetPlayerById(playerID);
            _ballMovement.KickBall(player);

            //await Clients.Client(Context.ConnectionId).SendAsync("KickBallResponse");
        }

        // Request to create or join certain colored team (currently red/blue depending on button pressed in client)
        public async Task JoinTeamRequest(string playerID, string colorName)
        {
            Color color = Color.FromName(colorName);
            Player newPlayer = new Player(new Vector2(0, 0), color, 50, 8);
            if (playerID.Equals(String.Empty))
            {
                playerID = Guid.NewGuid().ToString("N");
            }
            else
            {
                int teamIndex = _gameState.GetTeamIndexByPlayerID(playerID);
                _gameState.Teams[teamIndex].Players.Remove(playerID);
                await Clients.All.SendAsync("DeleteSpriteResponse", playerID);
                playerID = Guid.NewGuid().ToString("N");
            }

            int teamId = _gameState.GetTeamIndexByColor(color);
            if (teamId == -1)
            {
                Team newTeam = new Team(color);
                newTeam.Players.Add(playerID, newPlayer);
                _gameState.Teams.Add(newTeam);
            }
            else
            {
                _gameState.Teams[teamId].Players.Add(playerID, newPlayer);
            }

            await Clients.Client(Context.ConnectionId).SendAsync("JoinTeamResponse", playerID, color.Name);
        }
    }
}
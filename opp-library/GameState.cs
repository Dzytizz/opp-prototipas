#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace opp_library
{
    public class GameState
    {
        public List<Team> Teams { get; set; }
        public PlayerInput PlayerInput { get; set; }
        public Ball Ball { get; set; }

        public GameState()
        {
            this.Teams = new List<Team>();
            PlayerInput = new PlayerInput();
            Ball = new Ball(Vector2.Zero, Vector2.Zero, 1f, 20, 40);
        }

        public Player GetPlayerById(string playerID)
        {
            foreach (var team in Teams)
            {
                if (team.Players.ContainsKey(playerID))
                {
                    return team.Players[playerID];
                }
            }

            return null;
        }

        public int GetTeamIndexByPlayerID(string playerID)
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                foreach (KeyValuePair<string, Player> pair in Teams[i].Players)
                {
                    if (Teams[i].Players.ContainsKey(pair.Key))
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public int GetTeamIndexByColor(Color color)
        {
            for (int i = 0; i < Teams.Count; i++)
            {
                if (Teams[i].Color.Equals(color))
                {
                    return i;
                }
            }

            return -1;
        }

        public GameState Clone()
        {
            GameState newGameState = new GameState();
            List<Team> teams = new List<Team>();

            Ball ball = new Ball(this.Ball.Position, this.Ball.Velocity, this.Ball.Friction, this.Ball.KickSpeed,
                this.Ball.Radius);
            newGameState.Ball = ball;

            for (int i = 0; i < Teams.Count; i++)
            {
                Color color = Color.FromArgb(Teams[i].Color.A, Teams[i].Color.R, Teams[i].Color.G, Teams[i].Color.B);
                teams.Add(new Team(color));

                Dictionary<string, Player> players = new Dictionary<string, Player>();
                foreach (KeyValuePair<string, Player> pair in Teams[i].Players)
                {
                    Player player = new Player(pair.Value.Position, pair.Value.Color, pair.Value.Radius,
                        pair.Value.Speed);
                    players.Add(pair.Key, player);
                }

                teams[i].Players = players;
            }

            newGameState.Teams = teams;
            return newGameState;
        }
    }
}
#region (c) Kamuoliai 2022

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Numerics;
using opp_library;
using System.Timers;

namespace opp_server
{
    public class BallMovement
    {
        public Ball Ball { get; set; }
        public Timer BallLoop { get; set; }
        public Field Field { get; set; }

        public BallMovement()
        {
            BallLoop = new Timer(50);
            BallLoop.Elapsed += BallLoop_Elapsed;
            BallLoop.Enabled = true;
            BallLoop.AutoReset = true;
            BallLoop.Start();
        }

        private void BallLoop_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Ball.Velocity != Vector2.Zero)
            {
                HandleCollisions(Ball.Position, Field);

                // Acceleration
                Ball.Position += Ball.Velocity;

                // Deceleration
                Vector2 direction = Vector2.Normalize(Ball.Velocity);
                Ball.Velocity = new Vector2(Ball.Velocity.X - Ball.Friction * direction.X,
                    Ball.Velocity.Y - Ball.Friction * direction.Y);
                if (Ball.Velocity.Length() < 0.1f)
                {
                    Ball.Velocity = Vector2.Zero;
                }
            }
        }

        private void HandleCollisions(Vector2 position, Field field)
        {
            Walls walls = field.HitWalls(position, Ball.Radius);

            if (walls == Walls.Up || walls == Walls.Down)
            {
                Ball.Velocity *= new Vector2(1, -1);
            }

            if (walls == Walls.Left || walls == Walls.Right)
            {
                Ball.Velocity *= new Vector2(-1, 1);
            }
        }

        public void KickBall(Player player)
        {
            Vector2 playerPosition = player.Position;
            float distance = Vector2.Distance(playerPosition, Ball.Position);
            if (distance > Ball.Radius / 2 + player.Radius / 2 + 15)
            {
                return;
            }

            Vector2 direction = Ball.Position - playerPosition;
            Ball.Velocity = Vector2.Normalize(direction) * Ball.KickSpeed;
        }
    }
}
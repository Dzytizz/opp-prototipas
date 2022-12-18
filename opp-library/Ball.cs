#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Numerics;

namespace opp_library
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Friction { get; set; }
        public float KickSpeed { get; set; }
        public int Radius { get; set; }

        public Ball(Vector2 position, Vector2 velocity, float friction, float kickSpeed, int radius)
        {
            this.Position = position;
            this.Velocity = velocity;
            this.Friction = friction;
            this.KickSpeed = kickSpeed;
            this.Radius = radius;
        }
    }
}
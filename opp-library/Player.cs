#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Drawing;
using System.Numerics;

namespace opp_library
{
    public class Player
    {
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public int Radius { get; set; }
        public int Speed { get; set; }

        public Player(Vector2 position, Color color, int radius, int speed)
        {
            this.Position = position;
            this.Color = color;
            this.Radius = radius;
            this.Speed = speed;
        }

        public void Move(PlayerInput playerInput, Field field)
        {
            Vector2 newPosition = this.Position;

            if (playerInput.Up && playerInput.Left)
                newPosition += new Vector2(-Speed / 1.4142f, -Speed / 1.4142f);
            else if (playerInput.Up && playerInput.Right)
                newPosition += new Vector2(Speed / 1.4142f, -Speed / 1.4142f);
            else if (playerInput.Down && playerInput.Left)
                newPosition += new Vector2(-Speed / 1.4142f, Speed / 1.4142f);
            else if (playerInput.Down && playerInput.Right)
                newPosition += new Vector2(Speed / 1.4142f, Speed / 1.4142f);

            else if (playerInput.Up)
                newPosition += new Vector2(0, -Speed);
            else if (playerInput.Down)
                newPosition += new Vector2(0, Speed);
            else if (playerInput.Left)
                newPosition += new Vector2(-Speed, 0);
            else if (playerInput.Right)
                newPosition += new Vector2(Speed, 0);

            if (field.HitWalls(newPosition, this.Radius) == Walls.None)
            {
                this.Position = newPosition;
            }
        }
    }
}
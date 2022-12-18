#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Numerics;

namespace opp_library
{
    public class Field
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Field()
        {
        }

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        // Returns which wall the player is colliding with
        public Walls HitWalls(Player player)
        {
            Walls walls = Walls.None;

            if (player.Position.X <= 0)
            {
                walls |= Walls.Left;
            }

            if (player.Position.Y <= 0)
            {
                walls |= Walls.Up;
            }

            if (player.Position.X + player.Radius >= Width)
            {
                walls |= Walls.Right;
            }

            if (player.Position.Y + player.Radius >= Height)
            {
                walls |= Walls.Down;
            }

            return walls;
        }

        // Returns which wall the given round object is colliding with
        public Walls HitWalls(Vector2 objectPosition, int radius)
        {
            Walls walls = Walls.None;

            if (objectPosition.X <= 0)
            {
                walls |= Walls.Left;
            }

            if (objectPosition.Y <= 0)
            {
                walls |= Walls.Up;
            }

            if (objectPosition.X + radius >= Width)
            {
                walls |= Walls.Right;
            }

            if (objectPosition.Y + radius >= Height)
            {
                walls |= Walls.Down;
            }

            return walls;
        }
    }
}
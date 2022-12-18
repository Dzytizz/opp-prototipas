#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System.Collections.Generic;
using System.Drawing;

namespace opp_library
{
    public class Team
    {
        public Dictionary<string, Player> Players { get; set; }

        public Color Color { get; set; }

        public Team()
        {
            Players = new Dictionary<string, Player>();
        }

        public Team(Dictionary<string, Player> players, Color color)
        {
            Players = players;
            Color = color;
        }

        public Team(Color color)
        {
            Players = new Dictionary<string, Player>();
            Color = color;
        }
    }
}
#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

using System;

namespace opp_library
{
    // enum with binary flags (0/1/2/4/8)
    [Flags]
    public enum Walls
    {
        None = 0b_0000_0000,
        Up = 0b_0000_0001,
        Down = 0b_0000_0010,
        Left = 0b_0000_0100,
        Right = 0b_0000_1000,
    }
}
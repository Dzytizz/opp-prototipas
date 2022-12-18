#region (c) 2022 Kamuoliai.

// Futbolo žaidimo prototipas
// ---
// IFF-9/2 Vaiva Šafranavičiūtė, IFF-9/2 Gerda Žvirblytė, IFF-9/2 Paulius Petrauskas, IFF-9/2 Gytis Dokšas

#endregion

namespace opp_library
{
    public class PlayerInput
    {
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool Kick { get; set; }

        public PlayerInput()
        {
        }

        public bool IsActive()
        {
            return Up || Down || Left || Right;
        }
    }
}
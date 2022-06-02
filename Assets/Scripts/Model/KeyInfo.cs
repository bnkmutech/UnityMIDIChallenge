using UnityEngine;

namespace RhythmGame.Model
{
    public class KeyInfo
    {
        public KeyCode Key { get; set; }
        public Color TextColour { get; set; } = Color.white;
        public Color Colour { get; set; }

        public static KeyInfo operator +(KeyInfo a, KeyInfo b)
        {
            a.Key = b.Key == KeyCode.None ? a.Key : b.Key;
            a.TextColour = b.TextColour == new Color() ? a.TextColour : b.TextColour;
            a.Colour = b.Colour == new Color() ? a.Colour : b.Colour;
            return a;
        }
    }
}

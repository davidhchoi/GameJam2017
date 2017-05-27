using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017 {
    class Core {
        public static Game Game;
        public static Random rnd = new Random();
        public static SpriteFont freestyle12;

        public static void Initialize() {
            freestyle12 = Game.Content.Load<SpriteFont>("freestyle12");
        }

        public enum Colours {
            Red,
            Yellow,
            Blue,
            Green,
            Orange,
            Purple
        };

        public static Color ColourNameToColour(Colours color) {
            switch (color) {
                case Colours.Red:
                    return Color.Red;
                case Colours.Yellow:
                    return Color.Yellow;
                case Colours.Blue:
                    return Color.Blue;
                case Colours.Green:
                    return Color.Green;
                case Colours.Orange:
                    return Color.Orange;
                case Colours.Purple:
                    return Color.Purple;
            }
            throw new Exception("Something bad happened");
        }
    }
}

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
        public static Game1 Game;
        public static Random rnd = new Random();
        public static SpriteFont freestyle12;

        public static Texture2D [] Rectangles = new Texture2D[Enum.GetValues(typeof(Core.Colours)).Length];

        public enum Colours {
            Red,
            Yellow,
            Blue,
            Green,
            Orange,
            Purple,
            White
        };

        public static int ScreenWidth, ScreenHeight;

        public static void Initialize() {
            freestyle12 = Game.Content.Load<SpriteFont>("freestyle12");

            foreach (Core.Colours colour in Enum.GetValues(typeof(Core.Colours))) {
                Rectangles[(int)colour] = new Texture2D(Game.GraphicsDevice, 1, 1);
                Rectangles[(int)colour].SetData(new[] { ColourNameToColour(colour) });
            }
            ScreenWidth = Game.Width;
            ScreenHeight = Game.Height;
        }

        public static Texture2D ReColor(Texture2D source, Colours c) {

            Texture2D target = new Texture2D(Core.Game.GraphicsDevice, source.Width, source.Height);


            Color[] data = new Color[target.Width * target.Height];
            Core.Game.Content.Load<Texture2D>("cardback").GetData(data);

            Color newColor = ColourNameToColour(c);
            for (int x = 0; x < target.Width * target.Height; x++) {
                if (Math.Abs(data[x].R - data[x].B) < 20
                    && Math.Abs(data[x].R - data[x].G) < 20
                    && Math.Abs(data[x].G - data[x].B) < 20) {
                    continue;
                }
                double amount = data[x].B / 255.0;
                data[x].B = (byte)((newColor.B * amount));
                data[x].R = (byte)((newColor.R * amount));
                data[x].G = (byte)((newColor.G * amount));
            }
            Color tmp = Core.ColourNameToColour(c);

            target.SetData(data);
            return target;
        }

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
                case Colours.White:
                    return Color.White;
            }
            throw new Exception("Something bad happened");
        }
    }
}

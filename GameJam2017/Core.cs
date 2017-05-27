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

        public static Vector2 ToVector2(Point point) {
            return new Vector2(point.X, point.Y);
        }

        public static Texture2D ReColor(Texture2D source, Colours c) {

            Texture2D target = new Texture2D(Core.Game.GraphicsDevice, source.Width, source.Height);


            Color[] data = new Color[target.Width * target.Height];
            Core.Game.Content.Load<Texture2D>("cardback").GetData(data);

            int newColor = ColourNameToHue(c);
            for (int x = 0; x < target.Width * target.Height; x++) {
                RgbColor r = new RgbColor(data[x].R, data[x].G, data[x].B);
                HsvColor h = RgbToHsv(r);
                h.h = newColor;
                r = HsvToRgb(h);
                data[x].R = (byte)r.r;
                data[x].G = (byte)r.g;
                data[x].B = (byte)r.b;

            }

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

        public static int ColourNameToHue(Colours color) {
            switch (color) {
                case Colours.Red:
                    return 0;
                case Colours.Yellow:
                    return 60 * 256 / 360;
                case Colours.Blue:
                    return 240 * 256 / 360;
                case Colours.Green:
                    return 120 * 256 / 360;
                case Colours.Orange:
                    return 30 * 256 / 360;
                case Colours.Purple:
                    return 300 * 256 / 360;
                case Colours.White:
                    return 0;
            }
            throw new Exception("Something bad happened");
        }



        public struct RgbColor {
            public int r;
            public int g;
            public int b;

            public RgbColor(int r, int g, int b) {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }

        public struct HsvColor {
            public int h;
            public int s;
            public int v;

            public HsvColor(int r, int g, int b) {
                this.h = r;
                this.s = g;
                this.v = b;
            }
        }

        public static RgbColor HsvToRgb(HsvColor hsv) {
            RgbColor rgb;
            int region, remainder, p, q, t;

            if (hsv.s == 0) {
                rgb.r = hsv.v;
                rgb.g = hsv.v;
                rgb.b = hsv.v;
                return rgb;
            }

            region = (int)(hsv.h / 43);
            remainder = (int)((hsv.h - (region * 43)) * 6);

            p = (int)((hsv.v * (255 - hsv.s)) >> 8);
            q = (int)((hsv.v * (255 - ((hsv.s * remainder) >> 8))) >> 8);
            t = (int)((hsv.v * (255 - ((hsv.s * (255 - remainder)) >> 8))) >> 8);

            switch (region) {
                case 0:
                    rgb.r = hsv.v;
                    rgb.g = t;
                    rgb.b = p;
                    break;
                case 1:
                    rgb.r = q;
                    rgb.g = hsv.v;
                    rgb.b = p;
                    break;
                case 2:
                    rgb.r = p;
                    rgb.g = hsv.v;
                    rgb.b = t;
                    break;
                case 3:
                    rgb.r = p;
                    rgb.g = q;
                    rgb.b = hsv.v;
                    break;
                case 4:
                    rgb.r = t;
                    rgb.g = p;
                    rgb.b = hsv.v;
                    break;
                default:
                    rgb.r = hsv.v;
                    rgb.g = p;
                    rgb.b = q;
                    break;
            }

            return rgb;
        }

        public static HsvColor RgbToHsv(RgbColor rgb) {
            HsvColor hsv;
            int rgbMin, rgbMax;

            rgbMin = rgb.r < rgb.g ? (rgb.r < rgb.b ? rgb.r : rgb.b) : (rgb.g < rgb.b ? rgb.g : rgb.b);
            rgbMax = rgb.r > rgb.g ? (rgb.r > rgb.b ? rgb.r : rgb.b) : (rgb.g > rgb.b ? rgb.g : rgb.b);

            hsv.v = rgbMax;
            if (hsv.v == 0) {
                hsv.h = 0;
                hsv.s = 0;
                return hsv;
            }

            hsv.s = (int)(255 * (int)(rgbMax - rgbMin) / hsv.v);
            if (hsv.s == 0) {
                hsv.h = 0;
                return hsv;
            }

            if (rgbMax == rgb.r)
                hsv.h = (int)(0 + 43 * (rgb.g - rgb.b) / (rgbMax - rgbMin));
            else if (rgbMax == rgb.g)
                hsv.h = (int)(85 + 43 * (rgb.b - rgb.r) / (rgbMax - rgbMin));
            else
                hsv.h = (int)(171 + 43 * (rgb.r - rgb.g) / (rgbMax - rgbMin));

            return hsv;
        }
    }
}

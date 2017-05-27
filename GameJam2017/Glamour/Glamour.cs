using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    class Glamour : Entity {
        private GlamourColour c;
        private Shape s;
        private Effect e;
        private Alter [] a;

        public Glamour(GlamourColour c, Shape s, Effect e, Alter[] a, Rectangle pos) : base(pos, Vector2.Zero) {
            this.c = c;
            this.s = s;
            this.e = e;
            this.a = new Alter[a.Length];
            Array.Copy(this.a, a, a.Length);
        }

        public override String ToString() {
            String st = "";
            for (int i = 0; i < a.Length; i++) {
                st += a.ToString() + " ";
            }
            st += c.C.ToString() + " " + s.T.ToString() + " " + e.T.ToString();
            return st;
        }

        public override void Draw(GameTime g, SpriteBatch spriteBatch) {
            Rectangle destination = Pos;
            c.Draw(spriteBatch, destination);
            spriteBatch.Draw(Core.Rectangles[(int)Core.Colours.White], new Rectangle(
                destination.Left + destination.Width / 10, destination.Top + destination.Height * 6 / 10,
                destination.Width * 8 / 10, destination.Height * 3 / 10), Color.White * .4f);
            spriteBatch.DrawString(Core.freestyle12, ToString(),
                new Vector2(destination.Left, destination.Top), Color.Black);
        }

        public static Glamour RandomGlamour(Rectangle pos) {
            int i = Core.rnd.Next(GlamourColour.GlamourColours.Length);
            GlamourColour c = GlamourColour.GlamourColours[i];
            i = Core.rnd.Next(Shape.shapes.Length);
            Shape s = Shape.shapes[i];
            i = Core.rnd.Next(Effect.effects.Length);
            Effect e = Effect.effects[i];

            
            return new Glamour(c, s, e, new Alter[0], pos);
        }

        public static void Initialize() {
            GlamourColour.Initialize();
            Shape.Initialize();
            Effect.Initialize();
            Alter.Initialize();
        }
    }
}

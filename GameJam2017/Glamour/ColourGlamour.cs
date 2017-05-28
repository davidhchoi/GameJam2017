using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    class ColourGlamour : Glamour {
        public GlamourColour C { get; }

        public ColourGlamour(GlamourColour c, Vector2 pos, Vector2 size) : base(pos, size) {
            this.C = c;
        }

        public override String ToString() {
            String st = "";
            st += C.C.ToString() + " Colour Change";
            return st;
        }

        public override void Draw(GameTime g, SpriteBatch spriteBatch) {
            base.Draw(g, spriteBatch);
            Rectangle destination = new Rectangle(Pos.ToPoint(), Size.ToPoint());

            C.Draw(spriteBatch, destination);
            spriteBatch.Draw(Core.Rectangles[(int) Core.Colours.White], new Rectangle(
                destination.Left + destination.Width / 10, destination.Top + destination.Height * 6 / 10,
                destination.Width * 8 / 10, destination.Height * 3 / 10), Color.White * .8f);
            spriteBatch.DrawString(Core.freestyle12, ToString(),
                new Vector2(destination.Left + destination.Width / 8, destination.Top + destination.Height * 7 / 10),
                Color.Black, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
        }

        public override void Cast(Vector2 pos, float angle, Field f) {
            foreach (Controllable unit in f.Controllables) {
                if (unit.Faction == Unit.Unit.Factions.P1) {
                    unit.ReColor(C.C);
                }
            }
        }
    }
}

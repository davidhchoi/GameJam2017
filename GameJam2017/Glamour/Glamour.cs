using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Glamour.Bullets;
using GameJam2017.Scene;
using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    public abstract class Glamour : Entity {
        public int Cost { get; protected set; }
        public GlamourColour C { get; protected set; }

        private Texture2D mana;

        protected Glamour(Vector2 pos, Vector2 size, GlamourColour c) : base(pos, size, Vector2.Zero) {
            C = c;
            mana = Core.GetRecoloredCache("mana", C.C);
        }


        public abstract override String ToString();

        public abstract String Description();


        public override void Draw(GameTime g, SpriteBatch spriteBatch) {
            base.Draw(g, spriteBatch);
            Rectangle destination = new Rectangle(Pos.ToPoint(), Size.ToPoint());

            if (Height > Width) {
                C.Draw(spriteBatch, destination);

                spriteBatch.Draw(Core.Rectangles[(int) Core.Colours.White], new Rectangle(
                    destination.Left + destination.Width / 15, destination.Top + destination.Height * 6 / 10,
                    destination.Width * 13 / 15, destination.Height * 3 / 10), Color.White * .8f);
                spriteBatch.DrawString(Core.freestyle12,
                    Core.WrapText(Core.freestyle12, Description(), destination.Width * 12 / 15),
                    new Vector2(destination.Left + destination.Width / 15 + 5,
                        destination.Top + destination.Height * 6 / 10 + 5),
                    Color.Black, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);

                spriteBatch.Draw(Core.Rectangles[(int) Core.Colours.White], new Rectangle(
                    destination.Left + destination.Width / 15, destination.Top + destination.Height / 20,
                    destination.Width * 13 / 15, Core.freestyle12.LineSpacing - 4), Color.White * .8f);
                spriteBatch.DrawString(Core.freestyle12,
                    Core.WrapText(Core.freestyle12, ToString(), destination.Width * 12 / 15),
                    new Vector2(destination.Left + destination.Width / 15 + 3,
                        destination.Top + destination.Height / 20 + 3),
                    Color.Black, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);

                int curY = destination.Top + destination.Height / 20 + Core.freestyle12.LineSpacing;
                int curX = destination.Left + destination.Width / 15 + 2;
                if (Cost > 0) {
                    spriteBatch.Draw(Core.Rectangles[(int)Core.Colours.White], new Rectangle(
                        curX - 2, curY - 2,
                        destination.Width * 13 / 15, destination.Height / 20), Color.White * .8f);
                }
                for (int i = 0; i < Cost; i++) {
                    spriteBatch.Draw(mana, new Rectangle(curX, curY, destination.Height / 20 - 4, destination.Height / 20 - 4), Color.White);
                    curX += destination.Height / 20;
                    if (curX > destination.Left + destination.Width * 13 / 15) {
                        curY += destination.Height / 20 ;
                        curX = destination.Left + destination.Width / 15 + 2;
                        spriteBatch.Draw(Core.Rectangles[(int)Core.Colours.White], new Rectangle(
                            curX, curY,
                            destination.Width * 13 / 15, Core.freestyle12.LineSpacing - 4), Color.White * .8f);
                    }
                }
            } else {
                C.Draw(spriteBatch, destination);
                spriteBatch.Draw(Core.Rectangles[(int)Core.Colours.White], new Rectangle(
                    destination.Left + destination.Width / 15, destination.Top + destination.Height / 15,
                    destination.Width * 13 / 15, destination.Height * 13 / 15), Color.White * .8f);
                spriteBatch.DrawString(Core.freestyle12,
                    Core.WrapText(Core.freestyle12, ToString(), destination.Width * 12 / 15),
                    new Vector2(destination.Left + destination.Width / 15 + 3,
                        destination.Top + destination.Height / 20 + 2),
                    Color.Black, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0);
            }
        }

        public abstract void Cast(Vector2 pos, float angle, Field f);

        public static Glamour RandomSpellGlamour(Vector2 pos, Vector2 size, int maxCost, GlamourColour curColour) {
            int i;
            for (int num = 0; num < 1000; num++) {
                i = Core.rnd.Next(Effect.effects.Length);
                Effect e = Effect.effects[i];
                if (e.T == Effect.Type.MindControl) continue;
                i = Core.rnd.Next(Shape.shapes.Length);
                Shape s = Shape.shapes[i];

                SpellGlamour g = new SpellGlamour(curColour, s, e, new Alter[0], pos, size);
                if (g.Cost < maxCost)
                    return g;
            }
            return new SpellGlamour(curColour, Shape.shapes[(int)Shape.Type.Bullet], Effect.effects[(int)Effect.Type.Damage],
                new Alter[0], pos, size);
        }

        public static Glamour RandomColourGlamour(Vector2 pos, Vector2 size, int [] allowedColours) {
            int i = Core.rnd.Next(allowedColours.Length);
            GlamourColour c = GlamourColour.GlamourColours[allowedColours[i]];

            return new ColourGlamour(c, pos, size);
        }

        public static void Initialize() {
            GlamourColour.Initialize();
            Shape.Initialize();
            Effect.Initialize();
            Alter.Initialize();
        }
    }
}

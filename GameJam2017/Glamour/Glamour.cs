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

        protected Glamour(Vector2 pos, Vector2 size) : base(pos, size, Vector2.Zero) {
        }


        public abstract override String ToString();

        public override void Draw(GameTime g, SpriteBatch spriteBatch) {
        }

        public abstract void Cast(Vector2 pos, float angle, Field f);

        public static Glamour RandomSpellGlamour(Vector2 pos, Vector2 size, int maxCost, GlamourColour curColour) {
            int i;
            for (int num = 0; num < 1000; num++) {
                i = Core.rnd.Next(Effect.effects.Length);
                Effect e = Effect.effects[i];
                i = Core.rnd.Next(Shape.shapes.Length);
                Shape s = Shape.shapes[i];

                SpellGlamour g = new SpellGlamour(curColour, s, e, new Alter[0], pos, size);
                if (g.Cost < maxCost)
                    return g;
            }
            return new SpellGlamour(curColour, Shape.shapes[(int)Shape.Type.Bullet], Effect.effects[(int)Effect.Type.MindControl],
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    public class Deck : Entity {
        List<Glamour> glamours = new List<Glamour>();

        public GlamourColour LastColour { get; private set; }
        public int[] MaxCosts { get; private set; }
        public int NumSpells { get; private set; }

        public Deck(Vector2 pos, Vector2 size) : base(pos, size, Vector2.Zero) {
            MaxCosts = new int[Enum.GetValues(typeof(Core.Colours)).Length];
            for (int i = 0; i < MaxCosts.Length; i++) {
                MaxCosts[i] = 0;
            }
        }

//        public override void Reset() {
//            glamours.Clear();
//            for (int i = 0; i < MaxCosts.Length; i++) {
//                MaxCosts[i] = 0;
//            }
//            base.Reset();
//        }

        public int Count { get { return glamours.Count;  } }

        public void AddGlamour(Glamour g) {
            g.Pos = new Vector2(Pos.X, Pos.Y + Size.Y * glamours.Count / 30);
            g.Size = new Vector2(Size.X, Size.Y / 30);
            glamours.Add(g);

            ColourGlamour cg = g as ColourGlamour;
            if (cg != null) {
                LastColour = cg.C;
                MaxCosts[(int) cg.C.C]++;
                NumSpells = 0;
            } else {
                NumSpells++;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (var glamour in glamours) {
                glamour.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }

        public void CastNext(Vector2 pos, float angle, Field f) {
            if (glamours.Count == 0) return;
            glamours[0].Cast(pos, angle, f);
            glamours.RemoveAt(0);
        }
    }
}

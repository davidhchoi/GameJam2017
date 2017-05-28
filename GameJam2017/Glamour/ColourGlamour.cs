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

        public ColourGlamour(GlamourColour c, Vector2 pos, Vector2 size) : base(pos, size) {
            this.C = c;
        }

        public override String ToString() {
            String st = "";
            st += C.C.ToString() + " Colour Change";
            return st;
        }

        public override string Description() {
            return "Change allied colour to " + C.C + ". Gain one " + C.C + " mana.";
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

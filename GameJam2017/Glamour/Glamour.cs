using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Scene;

namespace GameJam2017.Glamour {
    class Glamour {
        private GlamourColour c;
        private Shape s;
        private Effect e;
        private Alter [] a;

        public Glamour(GlamourColour c, Shape s, Effect e, Alter[] a) {
            this.c = c;
            this.s = s;
            this.e = e;
            Array.Copy(this.a, a, a.Length);
        }

        public static Glamour RandomGlamour() {
            int i = Core.rnd.Next(GlamourColour.GlamourColours.Length);
            GlamourColour c = GlamourColour.GlamourColours[i];
            i = Core.rnd.Next(Shape.shapes.Length);
            Shape s = Shape.shapes[i];
            i = Core.rnd.Next(Effect.effects.Length);
            Effect e = Effect.effects[i];

            
            return new Glamour(c, s, e, new Alter[0]);
        }

        public static void Initialize() {
            GlamourColour.Initialize();
            Shape.Initialize();
            Effect.Initialize();
            Alter.Initialize();
        }
    }
}

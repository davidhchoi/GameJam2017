using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2017.Glamour.Bullets {
    class StatusEffect {
        private bool isColorEffect;
        private Core.Colours colour;

        public StatusEffect(Core.Colours colour) {
            this.colour = colour;
            isColorEffect = true;
        }

        public void Apply(Unit.Unit u) {
            
        }
    }
}

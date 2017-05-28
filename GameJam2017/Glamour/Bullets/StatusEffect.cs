using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2017.Glamour.Bullets {
    public class StatusEffect {
        private bool isColorEffect;
        public Core.Colours Colour { get; }

        public int Strength { get; }
        public int Duration { get; private set; }


        public StatusEffect(int strength, int duration, Core.Colours colour) {
            this.Strength = strength;
            this.Duration = duration;
            this.Colour = colour;
            isColorEffect = true;
        }

        public void Update() {
            Duration--;
        }
    }
}

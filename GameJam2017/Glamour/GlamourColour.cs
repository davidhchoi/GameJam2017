﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    public class GlamourColour {
        public static GlamourColour [] GlamourColours = new GlamourColour[Enum.GetValues(typeof(Core.Colours)).Length];

        public Core.Colours C { get; }
        private Texture2D cardBack;
        protected GlamourColour(Core.Colours c) {
            this.C = c;
            cardBack = Core.GetRecoloredCache("cardback", c);
        }

        public int Cost() {
            return 0;
        }

        public static void Initialize() {
            foreach (Core.Colours colour in Enum.GetValues(typeof(Core.Colours))) { 
                GlamourColours[(int)colour] = new GlamourColour(colour);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination) {
            spriteBatch.Draw(cardBack, destination, Color.White);
        }
    }
}

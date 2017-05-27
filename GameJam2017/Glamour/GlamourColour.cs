using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    class GlamourColour {
        public static GlamourColour [] GlamourColours = new GlamourColour[6];

        private Core.Colours c;
        private Texture2D cardBack;
        protected GlamourColour(Core.Colours c) {
            this.c = c;
            cardBack = Core.Game.Content.Load<Texture2D>("cardback");

            Color[] data = new Color[cardBack.Width * cardBack.Height];
            cardBack.GetData(data);

            for (int x = 0; x < cardBack.Width * cardBack.Height; x++) {
                if (data[x] == Color.Black) {
                    data[x] = Core.ColourNameToColour(c);
                }
            }

            cardBack.SetData(data);
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

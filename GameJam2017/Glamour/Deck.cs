using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Glamour {
    class Deck : Entity {
        List<Glamour> glamours = new List<Glamour>();

        public Deck(Rectangle pos) {
            Pos = pos;
        }

        public void AddGlamour(Glamour g) {
            g.Pos = new Rectangle(Pos.X, Pos.Y + Pos.Height * glamours.Count / 30, Pos.Width, Pos.Height / 30);
            glamours.Add(g);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            foreach (var glamour in glamours) {
                glamour.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}

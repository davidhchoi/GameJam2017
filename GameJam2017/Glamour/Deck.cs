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

        public Deck(Vector2 pos, Vector2 size) : base(pos, size, Vector2.Zero) {
        }

        public void AddGlamour(Glamour g) {
            g.Pos = new Vector2(Pos.X, Pos.Y + Size.X * glamours.Count / 30);
            g.Size = new Vector2(Size.X, Size.Y / 30);
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

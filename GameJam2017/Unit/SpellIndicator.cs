using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class SpellIndicator : Entity {
        private float progress;
        private Texture2D texture;
        private Color[] data;

        public SpellIndicator(Vector2 pos, Vector2 height) : base(pos, height, Vector2.Zero) {
            texture = new Texture2D(Core.Game.GraphicsDevice, Width, Height);
            this.data = new Color[Width * Height];
        }

        public void UpdateProgress(double progress) {
            double radius = Width / 2;
            for (int x = 0; x < Width; x++) {
                for (int y = 0; y < Height; y++) {
                    var diff = new Vector2(x - Width / 2, y - Height / 2);
                    var angle = Math.Atan2(diff.X, diff.Y) + Math.PI;
                    if (diff.Length() > .8 * (radius) && diff.Length() < radius && angle / (Math.PI * 2) < progress) {
                        data[x + y * Width] = Color.Black;
                    } else {
                        data[x + y * Width] = Color.Transparent;
                    }
                }
            }
            texture.SetData(data);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(texture, new Rectangle((Pos - Size / 2).ToPoint(), Size.ToPoint()), Color.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Cursor {
        public Texture2D CursorTexture;
        public Vector2 Position;

        public void Initialize(Vector2 position) {
            CursorTexture = Core.Game.Content.Load<Texture2D>("Units\\cursor");
            Position = position;
        }

        public Vector2 getPos() {
            return Position;
        }

        public void Update(GameTime time, Vector2 pos) {
            Position = pos;
        }
        public void Draw(SpriteBatch sb) {
            sb.Draw(CursorTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

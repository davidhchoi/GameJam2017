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
        public bool Active;
        float playerMoveSpeed;

        public void Initialize(Texture2D texture, Vector2 position) {
            CursorTexture= texture;
            Position = position;
        }

        public void Update(GameTime time, Vector2 pos) {
            Position = pos;
        }
        public void Draw(SpriteBatch sb) {
            sb.Draw(CursorTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

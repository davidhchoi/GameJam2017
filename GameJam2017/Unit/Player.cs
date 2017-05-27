using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Player {
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public Vector2 Target;
        public bool Active;
        float playerMoveSpeed;

        public int Width { get { return PlayerTexture.Width; } }
        public int Height { get { return PlayerTexture.Height; } }

        public void Initialize(Texture2D texture, Vector2 position) {
            PlayerTexture = texture;
            Position = position;
            Active = true;
            playerMoveSpeed = 8.0f;
        }

        public void ChangeTarget(Vector2 target) {
            Target = target;
        }

        public void Update(GameTime time) {
            var diff = Target - Position;
            diff.Normalize();
            Position = Position + diff * playerMoveSpeed;
        }
        public void Draw(SpriteBatch sb) {
            sb.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}

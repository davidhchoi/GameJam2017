using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Cursor {
        public Vector2 Position;

        public void Initialize(Vector2 position) {
            Position = position;
        }

        public Vector2 getPos() {
            return Position;
        }

        public void Update(GameTime time, Vector2 pos) {
            Position = pos;
        }
        public void Draw(SpriteBatch sb) {
        }
    }
}

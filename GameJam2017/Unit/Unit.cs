using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    /**
     * Represents any unit on the field
     */
    abstract class Unit {
        protected Texture2D Texture;
        protected Vector2 Position;
        protected Vector2 Target;
        protected float MoveSpeed;

        public int Width { get { return Texture.Width; } }
        public int Height { get { return Texture.Height; } }

        public virtual void Initialize(Texture2D texture, Vector2 position, float movespeed) {
            Texture = texture;
            Position = position;
            MoveSpeed = movespeed;
        }

        public Vector2 getPos() { return Position + new Vector2(Width, Height); }

        abstract public void Update(GameTime time);

        public void Draw(SpriteBatch sb) {
            sb.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

    }
}

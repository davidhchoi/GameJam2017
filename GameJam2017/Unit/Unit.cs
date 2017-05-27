using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Content;

namespace GameJam2017.Unit {
    /**
     * Represents any unit on the field
     */
    abstract class Unit : Entity {
        protected Texture2D Texture;
        protected float MoveSpeed;
        protected float angle;

        public int Width { get { return (int)Size.X; } }
        public int Height { get { return (int)Size.Y; } }

        public Unit() : base(Vector2.Zero, Vector2.Zero, Vector2.Zero) {
            
        }

        public virtual void Initialize(Texture2D texture, Vector2 position, float movespeed) {
            Texture = texture;
            Pos = position;
            Size = new Vector2(Texture.Width, Texture.Height);
            MoveSpeed = movespeed;
            angle = 0;
        }

        public Vector2 getPos() { return new Vector2(Pos.X + Width / 2, Pos.Y + Height / 2); }

        abstract public void Update(GameTime time, List<Unit> other);

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Texture, new Rectangle(Pos.ToPoint(), Size.ToPoint()), null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}

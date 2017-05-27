using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Content {
    public class Entity {
        public Vector2 Vel { get; set; }
        public Vector2 Pos { get; set; }
        public Vector2 Size { get; set; }

        public int Width { get { return (int)Size.X; } }
        public int Height { get { return (int)Size.Y; } }
        public int X { get { return (int)Pos.X; } }
        public int Y { get { return (int)Pos.Y; } }

        public Entity(Vector2 pos, Vector2 size, Vector2 vel) {
            this.Pos = pos;
            this.Size = size;
            this.Vel = vel;
        }

        public virtual void Initialize() {
        }

        public virtual void LoadContent() {
        }

        public virtual void UnloadContent() {
        }

        public virtual void Reset() {
        }

        public virtual void Update(GameTime gameTime) {
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        }

        public virtual bool Intersects(Point pos) {
            if (Width == Height) {
                return new Circle(X + Width / 2, Y + Height / 2, Width / 2).ContainsPoint(pos);
            }
            return new Rectangle(Pos.ToPoint(), Size.ToPoint()).Intersects(new Rectangle(pos, Point.Zero));
        }

        public virtual bool Intersects(Rectangle r) {
            if (Width == Height) {
                return new Circle(X + Width / 2, Y + Height / 2, Width / 2).Intersects(r);
            }
            return new Rectangle(Pos.ToPoint(), Size.ToPoint()).Intersects(r);
        }
    }
}

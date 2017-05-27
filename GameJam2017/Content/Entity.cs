using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Content {
    class Entity {
        public Rectangle Pos { get; set; }
        public Vector2 Vel { get; set;  }

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
    }
}

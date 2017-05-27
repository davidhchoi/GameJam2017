using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;

namespace GameJam2017.Scene {
    class Scene : IDrawable {
        List<Entity> entities = new List<Entity>();

        public virtual void Initialize() {
            foreach (var entity in entities) {
                entity.Initialize();
            }
        }

        public virtual void LoadContent() {
            foreach (var entity in entities) {
                entity.LoadContent();
            }
        }

        public virtual void UnloadContent() {
            foreach (var entity in entities) {
                entity.UnloadContent();
            }
        }

        public virtual void Reset() {
            foreach (var entity in entities) {
                entity.Reset();
            }
        }

        public virtual void Update(GameTime gameTime) {
            foreach (var entity in entities) {
                entity.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        }
    }
}

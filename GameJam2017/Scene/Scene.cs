using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;

namespace GameJam2017.Scene {
    public enum SceneTypes {
        Deck,
        Game,
        MainMenu
    }

    public class Scene : IDrawable {
        public List<Entity> entities = new List<Entity>();
        public List<Entity> toAddEntities = new List<Entity>();

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

        public virtual void MakeActive(GameTime gameTime) {
            foreach (var entity in entities) {
                entity.Reset();
            }
        }

        public virtual void Update(GameTime gameTime) {
            foreach (var entity in entities) {
                entity.Update(gameTime);
            }
            foreach(var entity in toAddEntities) {
                entities.Add(entity);
            }
            toAddEntities.Clear();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
        }
    }
}

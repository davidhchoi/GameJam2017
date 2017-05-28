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
        public HashSet<Entity> entities = new HashSet<Entity>();

        public HashSet<Entity> toAdd = new HashSet<Entity>();
        public HashSet<Entity> toRemove = new HashSet<Entity>();


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
            foreach (var entity in toRemove) {
                entities.Remove(entity);
            }
            foreach (var entity in toAdd) {
                entities.Add(entity);
            }
            toAdd.Clear();
            toRemove.Clear();
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            // TODO: this is garbage. as is a lot of this code
            foreach (var entity in entities) {
                Particle.Particle p = entity as Particle.Particle;
                if (p != null) {
                    p.Draw(gameTime, spriteBatch);
                }
            }
        }

        public virtual void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
        }
    }
}

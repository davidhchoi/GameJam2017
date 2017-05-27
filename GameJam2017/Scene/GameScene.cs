using GameJam2017.Unit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam2017.Scene {
    class GameScene : Scene {
        SpriteBatch spriteBatch;
        Field field;
        
        public GameScene() {
        }
        public override void Initialize() {
            base.Initialize();
            field = new Field(this);
            field.Initialize();
            entities.Add(field);
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            var mousePosition = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            if (Core.Game.MouseRightBecame(ButtonState.Pressed)) {
                field.RightClick(mousePosition);
            }
            if (Core.Game.MouseLeftBecame(ButtonState.Pressed)) {
                field.BeginSelection(mousePosition);
            }
            if (Core.Game.MouseLeftBecame(ButtonState.Released)) {
                field.EndSelection(mousePosition);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            field.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

    }
}

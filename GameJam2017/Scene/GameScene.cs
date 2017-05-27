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

        MouseState currentMouseState;
        MouseState prevMouseState;
        public GameScene() {
        }
        public override void Initialize() {
            base.Initialize();
            Vector2 playerPos = new Vector2(Core.Game.GraphicsDevice.Viewport.TitleSafeArea.X + Core.Game.GraphicsDevice.Viewport.TitleSafeArea.Width/2, Core.Game.GraphicsDevice.Viewport.TitleSafeArea.Y + Core.Game.GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            field = new Field();
            field.Initialize(playerPos);
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            var mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            if (currentMouseState.RightButton == ButtonState.Pressed) {
                field.RightClick(mousePosition);
            }
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton != ButtonState.Pressed
                ) {
                field.BeginSelection(mousePosition);
            }
            if (currentMouseState.LeftButton == ButtonState.Released && 
                prevMouseState.LeftButton != ButtonState.Released
                ) {
                field.EndSelection(mousePosition);
            }
            field.Update(gameTime, mousePosition);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            field.Draw(spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

    }
}

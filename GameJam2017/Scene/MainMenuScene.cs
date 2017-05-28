using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Component;

namespace GameJam2017.Scene {
    class MainMenuScene : Scene {
        private Button toDraft;
        Texture2D title, logo, logoSpin;
        private int time;
        public override void Initialize() {
            toDraft = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Deck);
            }, new Vector2(Core.ScreenWidth / 2 - 100, Core.ScreenHeight - 100), new Vector2(200, 60));
            entities.Add(toDraft);
            base.Initialize();
            toDraft.Initialize();

            // Set up the current level
            Core.currentLevel = 1;
        }

        public override void LoadContent() {
            base.LoadContent();
            title = Core.Game.Content.Load<Texture2D>("mainmenu");
            logo = Core.Game.Content.Load<Texture2D>("logo");
            logoSpin = Core.Game.Content.Load<Texture2D>("logorotate");
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            Core.Game.camera.CenterOn(new Vector2(1920 / 2, 1080 / 2));
            time = time + 1;
            base.Update(gameTime);
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(title, new Vector2(0, 0));
            toDraft.Draw(gameTime, spriteBatch);
            spriteBatch.Draw(logo, new Rectangle(Core.ScreenWidth / 2 - 150, Core.ScreenHeight / 2 - 150 + 80, 300, 300), Color.White);
            spriteBatch.Draw(logoSpin, new Rectangle(Core.ScreenWidth / 2, Core.ScreenHeight / 2 + 80, 600, 600), null, 
                Color.White, (float)(time % 360) / 360.0f * (float)Math.PI * 2, new Vector2(150, 150), SpriteEffects.None, 0f);
            
            base.Draw(gameTime, spriteBatch);
        }
    }
}

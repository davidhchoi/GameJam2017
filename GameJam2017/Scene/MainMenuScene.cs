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
        Texture2D title;
        public override void Initialize() {
            toDraft = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Deck);
            }, new Vector2(Core.ScreenWidth / 2 - 100, Core.ScreenHeight * 3 / 4), new Vector2(200, 60));
            entities.Add(toDraft);
            title = Core.Game.Content.Load<Texture2D>("mainmenu");
            base.Initialize();
            toDraft.Initialize();
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            Core.Game.camera.CenterOn(new Vector2(1920 / 2, 1080 / 2));
            base.Update(gameTime);
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(title, new Vector2(0, 0));
            toDraft.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

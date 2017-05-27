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
        public override void Initialize() {
            toDraft = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Deck);
            }, new Rectangle(Core.ScreenWidth / 2 - 100, Core.ScreenHeight * 3 / 4, 200, 60));
            entities.Add(toDraft);
            base.Initialize();
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            toDraft.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

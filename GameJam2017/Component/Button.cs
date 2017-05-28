using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJam2017.Component {
    class Button : Entity {
        private Action callback;

        enum BState {
            Unpressed,
            Pressed
        }

        private BState state;
        private Texture2D texture;
        private String Texture;

        public void Initialize() {
            texture = Core.Game.Content.Load<Texture2D>(Texture);
        }

        public Button(Action callback, Vector2 pos, Vector2 size) : base(pos, size, Vector2.Zero) {
            this.callback = callback;
            state = BState.Unpressed;
            Texture = "button";
        }

        public Button(Action callback, Vector2 pos, Vector2 size, String texture) : base(pos, size, Vector2.Zero) {
            this.callback = callback;
            state = BState.Unpressed;
            Texture = texture;
        }

        public override void Update(GameTime gameTime) {
            switch (state) {
                case BState.Unpressed:
                    if (Core.Game.MouseLeftBecame(ButtonState.Pressed) && Intersects(Mouse.GetState().Position)) {
                        state = BState.Pressed;
                    }
                    break;
                case BState.Pressed:
                    if (!Intersects(Mouse.GetState().Position)) state = BState.Unpressed;
                    else if (Core.Game.MouseLeftBecame(ButtonState.Released)) {
                        callback();
                    }
                    break;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, Pos, state == BState.Unpressed ? Color.White : (Color.White * .7f));
            base.Draw(gameTime, spriteBatch);
        }
    }
}

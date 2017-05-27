using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameJam2017.Component;
using GameJam2017.Glamour;
using Microsoft.Xna.Framework.Input;

namespace GameJam2017.Scene {
    class DraftScene : Scene {
        private Glamour.Glamour[] activeCards = new Glamour.Glamour[3];
        private Deck deck;
        private Button toGame;
        public override void Initialize() {
            deck = new Deck(new Vector2((int)(Core.ScreenWidth * .8), 50), new Vector2((int)(Core.ScreenWidth * .2), Core.ScreenHeight - 100));
            toGame = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Game);
            }, new Vector2(Core.ScreenWidth / 2 - 100, Core.ScreenHeight * 3 / 4), new Vector2(200, 60));
            entities.Add(deck);
            entities.Add(toGame);
            base.Initialize();
        }

        public override void LoadContent() {
            base.LoadContent();
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        protected void GenerateCards() {
            for (int i = 0; i < activeCards.Length; i++) {
                activeCards[i] = Glamour.Glamour.RandomGlamour(new Vector2(50 + i * 400, 50), new Vector2(300, 420));
                entities.Add(activeCards[i]);
            }
        }

        public override void Reset() {
            GenerateCards();
            base.Reset();
        }

        public override void Update(GameTime gameTime) {
            if (Core.Game.MouseLeftBecame(ButtonState.Pressed)) {
                for (int i = 0; i < activeCards.Length; i++) {
                    if (activeCards[i].Intersect(Mouse.GetState().Position)) {
                        deck.AddGlamour(activeCards[i]);
                        GenerateCards();
                    }
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            for (int i = 0; i < activeCards.Length; i++) {
                activeCards[i].Draw(gameTime, spriteBatch);
            }
            deck.Draw(gameTime, spriteBatch);
            toGame.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

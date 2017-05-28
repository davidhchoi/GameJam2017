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
        private List<Glamour.Glamour> activeCards = new List<Glamour.Glamour>();
        private Deck deck;
        private Button toGame;
        private int numDrafted;
        const int DECK_MULTIPLIER = 20;

        private int NumCardsEachDraft = 3;
        private Texture2D title;
        private int getMaxDeckSize() {
            return DECK_MULTIPLIER * Core.currentLevel;
        }

        public override void Initialize() {
            deck = new Deck(new Vector2((int)(Core.ScreenWidth * .8), 50), new Vector2((int)(Core.ScreenWidth * .2), Core.ScreenHeight - 100));
            toGame = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Game);
                Core.Game.gameScene.SetDeck(deck);
            }, new Vector2(Core.ScreenWidth / 2 - 100, Core.ScreenHeight - 100), new Vector2(200, 60));
            entities.Add(deck);
            entities.Add(toGame);
            base.Initialize();
            toGame.Initialize();
            toGame.Disable();
        }

        public override void LoadContent() {
            base.LoadContent();
            title = Core.Game.Content.Load<Texture2D>("draft");
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }

        protected void GenerateCards() {
            for (int i = 0; i < activeCards.Count; i++) {
                entities.Remove(activeCards[i]);
            }
            activeCards.Clear();
            Glamour.Glamour g;
            for (int i = 0; i < 2; i++) {
                g = Glamour.Glamour.RandomColourGlamour(new Vector2(50 + i * 400, 50), new Vector2(300, 420), Core.AllowedColours);
                activeCards.Add(g);
                entities.Add(g);
            }
            if (deck.Count > 0) {
                for (int i = 0; i < 3 - deck.NumSpells; i++) {
                    g = Glamour.Glamour.RandomSpellGlamour(new Vector2(50 + i * 400, 500), new Vector2(300, 420), 
                        deck.MaxCosts[(int)deck.LastColour.C], deck.LastColour);
                    activeCards.Add(g);
                    entities.Add(g);
                }
            }
        }

        public override void MakeActive(GameTime gameTime) {
            deck.Reset();
            GenerateCards();
            base.MakeActive(gameTime);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (Core.Game.MouseLeftBecame(ButtonState.Pressed)) {
                for (int i = 0; i < activeCards.Count; i++) {
                    if (activeCards[i].Intersects(Mouse.GetState().Position)) {
                        deck.AddGlamour(activeCards[i]);
                        if (++numDrafted >= getMaxDeckSize()) {
                            toGame.Enable();
                            activeCards.Clear();
                        } else {
                            GenerateCards();
                        }
                        return;
                    }
                }
            }
        }

        public override void DrawUI(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(title, new Vector2(0, 0));
            for (int i = 0; i < activeCards.Count; i++) {
                activeCards[i].Draw(gameTime, spriteBatch);
            }
            deck.Draw(gameTime, spriteBatch);
            toGame.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}

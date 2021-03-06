﻿using Microsoft.Xna.Framework;
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

        private int NumCardsEachDraft = 3;
        private Texture2D title;
        private int getMaxDeckSize() {
            return Core.DECK_MULTIPLIER * Core.currentLevel;
        }

        public override void Initialize() {
            toGame = new Button(delegate {
                Core.Game.ChangeScene(SceneTypes.Game);
                Core.Game.gameScene.SetDeck(deck);
            }, new Vector2(Core.ScreenWidth / 2 - 100, Core.ScreenHeight - 100), new Vector2(200, 60));
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
                g = Glamour.Glamour.RandomColourGlamour(new Vector2(50 + i * 400, 20), new Vector2(350, 490), Core.AllowedColours);
                activeCards.Add(g);
                entities.Add(g);
            }
            if (deck.Count > 0) {
                for (int i = 0; i < 3 - deck.NumSpells; i++) {
                    g = Glamour.Glamour.RandomSpellGlamour(new Vector2(50 + i * 400, 520), new Vector2(350, 490), 
                        deck.MaxCosts[(int)deck.LastColour.C], deck.LastColour);
                    activeCards.Add(g);
                    entities.Add(g);
                }
            }
        }

        public override void MakeActive(GameTime gameTime) {
            if (deck != null) entities.Remove(deck);
            deck = new Deck(new Vector2((int)(Core.ScreenWidth * .8), 50), new Vector2((int)(Core.ScreenWidth * .2), Core.ScreenHeight - 100));
            entities.Add(deck);
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

            int curX = 850;
            int curY = 50;
            for (int i = 0; i < Core.AllowedColours.Length; i++) {
                int colour = Core.AllowedColours[i];
                spriteBatch.DrawString(Core.freestyle16, ((Core.Colours)colour) + " Mana", new Vector2(curX, curY), Core.ColourNameToColour((Core.Colours)colour));
                curY += Core.freestyle16.LineSpacing;
                for (int j = 0; j < deck.MaxCosts[colour]; j++) {
                    spriteBatch.Draw(Core.GetRecoloredCache("mana", (Core.Colours)colour), new Rectangle(curX + 30 * j, curY, 25, 25), Color.White);
                }
                curY += 30;
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}

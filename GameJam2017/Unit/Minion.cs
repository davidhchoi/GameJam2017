using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameJam2017.Unit {
    class Minion : Controllable {
        public void Initialize(Vector2 position) {
            base.Initialize(Core.Game.Content.Load<Texture2D>("Units\\minion"), position, 5.0f);
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameJam2017.Unit {
    class Minion : Controllable {
        public Minion(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\minion"), position, 5.0f, f) { }
    }
}

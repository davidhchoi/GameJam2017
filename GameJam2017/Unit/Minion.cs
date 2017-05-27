using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameJam2017.Unit {
    class Minion : Controllable {
        public Minion(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\minion"), position, 5.0f, f) { }
        
        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, destinationRectangle: new Rectangle(Pos.ToPoint(), Size.ToPoint()), origin: new Vector2(Width / 2, Height / 2), rotation: angle);
        }
    }
}

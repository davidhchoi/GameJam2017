using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace GameJam2017.Unit {
    class Minion : Controllable {
        public void Initialize(Vector2 position) {
            base.Initialize(Core.Game.Content.Load<Texture2D>("Units\\minion"), position, 5.0f);
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, destinationRectangle: new Rectangle(Pos.ToPoint(), Size.ToPoint()), origin: new Vector2(Width/2, Height/2), rotation: angle); 
        }
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Player : Controllable {
        public void Initialize(Vector2 position) {
            base.Initialize(Core.Game.Content.Load<Texture2D>("Units\\player"), position, 8.0f);
        }

        public virtual Vector2 getPos() { return new Vector2(Pos.X + 22, Pos.Y + (Height-26)); }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, destinationRectangle: new Rectangle(Pos.ToPoint(), Size.ToPoint()), origin: new Vector2(22, Height-26), rotation: angle); 
        }

    }
}

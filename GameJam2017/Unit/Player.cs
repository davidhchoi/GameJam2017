using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    public class Player : Controllable {
        public Player(Vector2 position, Field f) : base(Core.Game.Content.Load<Texture2D>("Units\\player"), position, 8.0f, f) { }

        public virtual Vector2 getPos() { return new Vector2(Pos.X - 100 , Pos.Y -100); }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, destinationRectangle: new Rectangle(Pos.ToPoint(), Size.ToPoint()), origin: new Vector2(22, Height-26), rotation: Angle); 
        }

    }
}

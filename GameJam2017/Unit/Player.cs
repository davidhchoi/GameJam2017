using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    public class Player : Minion {
        public Player(Vector2 position, Field f) : base("Units\\player", position, Factions.P1, Core.Colours.White, f) {
            Health = 100;
        }
        int timeSinceLastShot = 0;

        public virtual Vector2 GetPos {
            get {return new Vector2(Pos.X - 100, Pos.Y -100);
            }
        }

        public override void Draw(SpriteBatch sb) {
            sb.Draw(Texture, new Vector2(Texture.Width / 3 + X, Texture.Height / 3 - 20 + Y), null, selected ? Color.White * .5f : Color.White, Angle, 
                new Vector2(Texture.Width / 3, Texture.Height * 2 / 3), 1f, SpriteEffects.None, 0f);
            sb.Draw(Core.Rectangles[2], new Rectangle((Pos + Size / 2 - new Vector2(5, 5)).ToPoint(), new Point(10, 10)), selected ? Color.White * .5f : Color.White);
        }

        public override void Kill() {
            Core.Game.ChangeScene(Scene.SceneTypes.MainMenu);
        }
    }
}

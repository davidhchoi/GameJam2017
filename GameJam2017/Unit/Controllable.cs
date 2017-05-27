using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    /**
     * Represents any unit which is controllable by the player
     */
    public abstract class Controllable : Unit {

        Vector2 Target;
        bool selected;


        public Controllable(Texture2D texture, Vector2 position, float movespeed, Field f) : base(texture, position, movespeed, f) {
            Target = position;
            Unselect();
        }

        public virtual void Select() {
            selected = true;
            Texture = Core.ReColor(Texture, Core.Colours.LightGreen);
        }
        public virtual void Unselect() {
            selected = false;
            Texture = Core.ReColor(Texture, Core.Colours.Green);
        }

        public void ChangeTarget(Vector2 target) {
            var toMid = new Vector2(Width / 2, Height / 2);
            Target = target - toMid;
        }

        public override void Update(GameTime time) {
            var diff = Target - Pos;
            if (diff.Length() > 5) {
                diff.Normalize();
                Pos = Pos + diff * MoveSpeed;
            }
            if (diff.X == 0) {
                Angle = 0;
            }
            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
        }
        
    }
}

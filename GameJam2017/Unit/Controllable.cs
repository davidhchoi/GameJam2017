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
    abstract class Controllable : Unit {

        Vector2 Target;
        public override void Initialize(Texture2D texture, Vector2 position, float movespeed) {
            base.Initialize(texture, position, movespeed);
            Target = position;
        }
        public void ChangeTarget(Vector2 target) {
            var toMid = new Vector2(Width / 2, Height / 2);
            Target = target - toMid;
        }

        public override void Update(GameTime time, List<Unit> other) {
            var diff = Target - Position;
            if (diff.Length() > 5) {
                diff.Normalize();
                Position = Position + diff * MoveSpeed;
            }
        }
        
    }
}

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
        Unit TargetEnemy;
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
<<<<<<< HEAD
            if (diff.X == 0) {
                Angle = 0;
            }
            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
=======
        }

        /**
         * Set the current strategy to stop
         */
        public void Stop() {
            currentStrategy = Strategy.STOP;
        }

        public void HoldPos() {
            currentStrategy = Strategy.HOLD_POS;
        }

        public void Attack(Unit u) {
            // If the target unit is an enemy, move to attack it
            if (u is Enemy) {
                currentStrategy = Strategy.ATTACK;
                TargetEnemy = u;
            } else { // Else if it's an ally, just move to it
                currentStrategy = Strategy.MOVE;
                Target = u.getPos();
            }
        }

        public void AttackMove(Vector2 target) {
            currentStrategy = Strategy.ATTACK_MOVE;
            Target = target;
        }

        public void Shoot(Unit u) {
            return;
        }

        public override void Update(GameTime time, List<Unit> other) {
            switch (currentStrategy) {
                case Unit.Strategy.ATTACK_MOVE:
                    if (Field.EnemyInRange(getPos())) {
                        Shoot(Field.GetNearestEnemy(getPos()));
                    } else {
                        var diff = Target - Pos;
                        if (diff.Length() > 5) {
                            diff.Normalize();
                            Pos = Pos + diff * MoveSpeed;
                            if (diff.X == 0) {
                                angle = 0;
                            }
                            angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                        }
                    }
                case Unit.Strategy.ATTACK:
                    if (Field.EnemyInRange(TargetEnemy)) {
                        Shoot(Field.GetNearestEnemy(getPos()));
                    } else {
                        var diff = Target - Pos;
                        if (diff.Length() > 5) {
                            diff.Normalize();
                            Pos = Pos + diff * MoveSpeed;
                            if (diff.X == 0) {
                                angle = 0;
                            }
                            angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                        }
                    }
                case Unit.Strategy.MOVE:
                    var diff = Target - Pos;
                    if (diff.Length() > 5) {
                        diff.Normalize();
                        Pos = Pos + diff * MoveSpeed;
                        if (diff.X == 0) {
                            angle = 0;
                        }
                        angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                    }
                    break;
                case Unit.Strategy.STOP:
                    break;
                case Unit.Strategy.HOLD_POS:
                    if (Field.EnemyInRange(getPos())) {
                        Shoot(Field.GetNearestEnemy(getPos()));
                    }
            }
>>>>>>> partial strategies
        }
    }
}

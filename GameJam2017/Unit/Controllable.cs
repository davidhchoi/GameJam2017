using System;
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
        int range = 200;


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

        public override void Update(GameTime time) {
            switch (currentStrategy) {
                case Unit.Strategy.ATTACK_MOVE:
                    Unit enemy = f.ClosestUnit(getPos());
                    Vector2 diff;
                    if (enemy != null && (enemy.getPos() - getPos()).Length() < range) {
                        Shoot(enemy);
                    } else {
                        diff = Target - Pos;
                        if (diff.Length() > 5) {
                            diff.Normalize();
                            Pos = Pos + diff * MoveSpeed;
                            if (diff.X == 0) {
                                Angle = 0;
                            }
                            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                        }
                    }
                    break;
                case Unit.Strategy.ATTACK:
                    if (f.IsEnemyInRange(getPos(), TargetEnemy, range)) {
                        Shoot(TargetEnemy);
                    } else {
                        diff = Target - Pos;
                        if (diff.Length() > 5) {
                            diff.Normalize();
                            Pos = Pos + diff * MoveSpeed;
                            if (diff.X == 0) {
                                Angle = 0;
                            }
                            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                        }
                    }
                    break;
                case Unit.Strategy.MOVE:
                    diff = Target - Pos;
                    if (diff.Length() > 5) {
                        diff.Normalize();
                        Pos = Pos + diff * MoveSpeed;
                        if (diff.X == 0) {
                            Angle = 0;
                        }
                        Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI);
                    }
                    break;
                case Unit.Strategy.STOP:
                    break;
                case Unit.Strategy.HOLD_POS:
                    enemy = f.ClosestUnit(getPos());
                    if (enemy != null && (enemy.getPos() - getPos()).Length() < range) {
                        Shoot(enemy);
                    }
                    break;
            }
        }
    }
}

using System;
using GameJam2017.Glamour.Bullets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    /**
     * Represents any unit which is controllable by the player
     */
    public abstract class Controllable : Unit {

        Vector2 Target;
        Unit TargetEnemy;
        protected int range = 200;


        public Controllable(string texture, Vector2 position, float movespeed, Factions faction, Core.Colours c, Field f) : 
            base(texture, position, movespeed, faction, c, f) {
            Target = position;
            Unselect();
        }

        public virtual void Select() {
            selected = true;
        }
        public virtual void Unselect() {
            selected = false;
        }

        public void Move(Vector2 target) {
            Target = target;
            currentStrategy = Strategy.MOVE;
            TargetEnemy = null;
        }

        /**
         * Set the current strategy to stop
         */
        public void Stop() {
            currentStrategy = Strategy.STOP;
            TargetEnemy = null;
        }

        public void HoldPos() {
            currentStrategy = Strategy.HOLD_POS;
            TargetEnemy = null;
        }

        public void Attack(Unit u) {
            // If the target unit is an enemy, move to attack it
            if (u.Faction != Faction) {
                currentStrategy = Strategy.ATTACK;
                TargetEnemy = u;
            } else { // Else if it's an ally, just move to it
                currentStrategy = Strategy.MOVE;
                TargetEnemy = u;
            }
        }

        public void AttackMove(Vector2 target) {
            currentStrategy = Strategy.ATTACK_MOVE;
            TargetEnemy = null;
            Target = target;
        }

        protected abstract void Shoot(Unit u);

        protected bool Move() {
            if (TargetEnemy != null) Target = TargetEnemy.GetPos;
            var diff = Target - Pos;
            if (diff.Length() < 5) return false;

            diff.Normalize();

            StatusEffect blue = colourStatusEffects[(int) Core.Colours.Blue];
            var speed = blue != null ? Math.Max(MoveSpeed - blue.Strength, 0) : MoveSpeed;
            Pos = Pos + diff * speed;
            Angle = (float)(Math.Atan2(diff.Y, diff.X) + Math.PI/2);
            return true;
        }

        public override void Update(GameTime time) {
            base.Update(time);
            // Stop attacking enemies if you are now allies
            if (TargetEnemy != null && (TargetEnemy.Health <= 0 || TargetEnemy.Colour == Colour)) Stop();
            Unit enemy = f.ClosestEnemy(this);
            switch (currentStrategy) {
                case Unit.Strategy.ATTACK_MOVE:
                    if (enemy != null && (enemy.GetPos - GetPos).Length() < range) {
                        Attack(enemy);
                    } else if (!Move()) {
                        Stop();
                    } else {
                        TargetEnemy = null;
                    }
                    break;
                case Unit.Strategy.ATTACK:
                    if ((TargetEnemy.GetPos - GetPos).Length() < range) {
                        Shoot(TargetEnemy);
                    } else if ((TargetEnemy.GetPos - GetPos).Length() > 2 * range) {
                        Stop();
                    } else {
                        Move();
                    }
                    break;
                case Unit.Strategy.MOVE:
                    if (!Move()) {
                        Stop();
                    }
                    break;
                case Unit.Strategy.STOP:
                    if (enemy != null && (enemy.GetPos - GetPos).Length() < range) {
                        Attack(enemy);
                    }
                    break;
                case Unit.Strategy.HOLD_POS:
                    if (enemy != null && (enemy.GetPos - GetPos).Length() < range) {
                        Shoot(enemy);
                    }
                    break;
            }
        }
    }
}

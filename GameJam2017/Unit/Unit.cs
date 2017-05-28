using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Content;
using GameJam2017.Glamour.Bullets;

namespace GameJam2017.Unit {
    /**
     * Represents any unit on the field
     */
    public abstract class Unit : Entity {
        protected Texture2D Texture;
        protected string TextureName;
        
        protected float MoveSpeed;
        public float Angle { get; set; }
        public int Health;
        public int MaxHealth;

        protected Field f;
        protected enum Strategy {
            ATTACK_MOVE,
            ATTACK,
            MOVE,
            STOP,
            HOLD_POS
        }

        protected Strategy currentStrategy;

        public enum Factions {
            P1,
            Enemy
        }
        public Factions Faction { get; set; }
        public Core.Colours Colour { get; set; }
        protected bool selected;

        protected StatusEffect[] colourStatusEffects = new StatusEffect[Enum.GetValues(typeof(Core.Colours)).Length];

        public Unit(string texture, Vector2 position, float movespeed, Factions factions, Core.Colours c, Field f) : 
            this(texture, position, new Vector2(Core.Game.Content.Load<Texture2D>(texture).Width, Core.Game.Content.Load<Texture2D>(texture).Height), movespeed, factions, c, f) {
        }

        public Unit(string texture, Vector2 position, Vector2 size, float movespeed, Factions factions, Core.Colours c, Field f) 
            : this(texture, Core.GetRecoloredCache(texture, c), position, size, movespeed, factions, c, f) {
        }

        public Unit(string textureName, Texture2D texture, Vector2 position, Vector2 size, float movespeed, Factions factions, Core.Colours c, Field f)
            : base(position, size, Vector2.Zero) {
            this.f = f;
            Texture = texture;
            TextureName = textureName;
            MoveSpeed = movespeed;
            Angle = 0;
            currentStrategy = Strategy.STOP;
            Faction = factions;
            Colour = c;
            Health = 1;
            MaxHealth = 1;
        }

        public virtual void ReColor(Core.Colours c) {
            Colour = c;
            Texture = Core.GetRecoloredCache(TextureName, c);
        }

        public virtual Vector2 GetPos {
            get { return new Vector2(Pos.X + Width / 2, Pos.Y + Height / 2); }
        }

        public override void Update(GameTime time) {
            for (int i = 0; i < colourStatusEffects.Length; i++) {
                if (colourStatusEffects[i] != null) {
                    colourStatusEffects[i].Update();
                    if (colourStatusEffects[i].Duration <= 0) {
                        colourStatusEffects[i] = null;
                    }
                }
            }
            Pos += Vel;

            StatusEffect red = colourStatusEffects[(int)Core.Colours.Red];
            if (red != null) {
                Health -= red.Strength;
            }
            if (Health <= 0) {
                Kill();
            }
            base.Update(time);
        }

        // Remove the unit from the game
        public abstract void Kill();

        public virtual void Draw(SpriteBatch sb) {
            // Draw the units
            sb.Draw(Texture, new Rectangle((Pos + Size / 2).ToPoint(), Size.ToPoint()), null, selected ? Color.White * .5f : Color.White, Angle, Size / 2, SpriteEffects.None, 0f);

            // Draw the health bars
            if (MaxHealth != 0) {
                sb.Draw(Core.Rectangles[2],
                    new Rectangle((Pos - new Vector2(0, 10)).ToPoint(),
                    new Point((int)(((float)Health) / MaxHealth * Width), 5)), Color.Red);
            }
        }

        public void AddStatus(StatusEffect s) {
            if (colourStatusEffects[(int)s.Colour] != null)
                colourStatusEffects[(int)s.Colour] = null;
            colourStatusEffects[(int) s.Colour] = s;
        }
    }
}

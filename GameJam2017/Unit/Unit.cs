using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameJam2017.Content;

namespace GameJam2017.Unit {
    /**
     * Represents any unit on the field
     */
    public abstract class Unit : Entity {
        protected Texture2D Texture;
        protected float MoveSpeed;
        public float Angle { get; set; }

        protected Field f;
        protected enum Strategy {
            ATTACK_MOVE,
            ATTACK,
            MOVE,
            STOP,
            HOLD_POS
        }

        protected Strategy currentStrategy;

        public int Width { get { return (int)Size.X; } }
        public int Height { get { return (int)Size.Y; } }

        public enum Factions {
            P1,
            Enemy
        }
        public Factions Faction { get; set; }
        public Core.Colours Colour { get; set; }
        protected bool selected;

        public Unit(string texture, Vector2 position, float movespeed, Factions factions, Core.Colours c, Field f) : 
            this(texture, position, new Vector2(Core.Game.Content.Load<Texture2D>(texture).Width, Core.Game.Content.Load<Texture2D>(texture).Height), movespeed, factions, c, f) {
        }

        public Unit(string texture, Vector2 position, Vector2 size, float movespeed, Factions factions, Core.Colours c, Field f) 
            : this(Core.GetRecoloredCache(texture, c), position, size, movespeed, factions, c, f) {
        }

        public Unit(Texture2D texture, Vector2 position, Vector2 size, float movespeed, Factions factions, Core.Colours c, Field f)
            : base(position, size, Vector2.Zero) {
            this.f = f;
            Texture = texture;
            MoveSpeed = movespeed;
            Angle = 0;
            currentStrategy = Strategy.STOP;
            Faction = factions;
            Colour = c;
        }

        public virtual Vector2 GetPos {
            get { return new Vector2(Pos.X + Width / 2, Pos.Y + Height / 2); }
        }

        public override void Update(GameTime time) {
//            Pos += new Vector2((float)Math.Sin(angle) * MoveSpeed, (float)Math.Cos(angle) * MoveSpeed);
            base.Update(time);
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Texture, new Rectangle((Pos + Size / 2).ToPoint(), Size.ToPoint()), null, selected ? Color.White * .5f : Color.White, Angle, Size / 2, SpriteEffects.None, 0f);
            sb.Draw(Core.Rectangles[2], new Rectangle((Pos + Size / 2 - new Vector2(5, 5)).ToPoint(), new Point(10, 10)), selected ? Color.White * .5f : Color.White);
        }
    }
}

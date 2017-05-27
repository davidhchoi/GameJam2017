﻿using System;
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
    abstract class Unit : Entity {
        protected Texture2D Texture;
        protected float MoveSpeed;
        protected float angle;

        protected Field f;

        public Unit(Texture2D texture, Vector2 position, float movespeed, Field f) : base(position, new Vector2(texture.Width, texture.Height), Vector2.Zero) {
            this.f = f;
            Texture = texture;
            MoveSpeed = movespeed;
            angle = 0;
        }

        public Unit(Texture2D texture, Vector2 position, Vector2 size, float movespeed, Field f) : base(position, size, Vector2.Zero) {
            this.f = f;
            Texture = texture;
            MoveSpeed = movespeed;
            angle = 0;
        }

        public virtual Vector2 getPos() { return new Vector2(Pos.X + Width / 2, Pos.Y + Height / 2); }

        public override void Update(GameTime time) {
//            Pos += new Vector2((float)Math.Sin(angle) * MoveSpeed, (float)Math.Cos(angle) * MoveSpeed);
            base.Update(time);
        }

        public virtual void Draw(SpriteBatch sb) {
            sb.Draw(Texture, new Rectangle(Pos.ToPoint(), Size.ToPoint()), null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}

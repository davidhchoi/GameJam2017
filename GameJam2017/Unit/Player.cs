﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Player : Controllable {
        public void Initialize(Vector2 position) {
            base.Initialize(Core.Game.Content.Load<Texture2D>("Units\\player"), position, 8.0f);
        }
    }
}

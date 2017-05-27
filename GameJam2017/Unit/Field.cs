using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameJam2017.Unit {
    class Field {
        int width = 1920;
        int height = 1080;
        Vector2 pos = new Vector2(0, 0);
        Player player;
        Cursor cursor;
        public Texture2D FieldTexture;

        public void Initialize(Texture2D fieldTexture, Texture2D playerTexture, Vector2 playerPos, Texture2D cursorTexture) {
            FieldTexture = fieldTexture;
            player = new Player();
            cursor = new Cursor();
            player.Initialize(playerTexture, playerPos);
            cursor.Initialize(cursorTexture, playerPos);
        }
        public void Click(Vector2 click) {
            player.ChangeTarget(click);
        }

        public void Update(GameTime time, Vector2 mousePos) {
            player.Update(time);
            cursor.Update(time, mousePos);
        }

        public void Draw(SpriteBatch sb) {
            sb.Draw(FieldTexture, pos, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            player.Draw(sb);
            cursor.Draw(sb);
        }
    }
}

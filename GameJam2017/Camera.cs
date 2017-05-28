
using Microsoft.Xna.Framework;

namespace GameJam2017 {
    public class Camera {
        public Camera() {

        }
        public Vector2 Position { get; private set; }

        public const int ViewportWidth = 1920;
        public const int ViewportHeight = 1080;
        const int MapWidth = 1920 * 2;
        const int MapHeight = 1080 * 2;

        // Center of the Viewport which does not account for scale
        public Vector2 ViewportCenter {
            get {
                return new Vector2(ViewportWidth * 0.5f, ViewportHeight * 0.5f);
            }
        }

        public Matrix TranslationMatrix {
            get {
                return Matrix.CreateTranslation(-(int)Position.X, -(int)Position.Y, 0) *
                    Matrix.CreateTranslation(new Vector3(ViewportCenter, 0));
            }
        }

        public void MoveCamera(Vector2 cameraMovement) {
            Vector2 newPosition = Position + cameraMovement;
            Position = MapClampedPosition(newPosition);
        }

        public Vector2 GetTopLeft() {
            return new Vector2(Position.X - 1920/2, Position.Y - 1080/2);
        }

        public void CenterOn(Vector2 position) {
            Position = MapClampedPosition(position);
        }

        // Clamp the camera so it never leaves the visible area of the map.
        private Vector2 MapClampedPosition(Vector2 position) {
            var cameraMax = new Vector2(MapWidth - ViewportWidth/ 2,
                MapHeight - ViewportHeight/ 2);

            return Vector2.Clamp(position,
               new Vector2(ViewportWidth / 2, ViewportHeight / 2), cameraMax);
        }

        public Vector2 WorldToScreen( Vector2 worldPosition) {
            return Vector2.Transform(worldPosition, TranslationMatrix);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition) {
            return Vector2.Transform(screenPosition, Matrix.Invert(TranslationMatrix));
        }

    }
}

namespace GameJam2017.Glamour {
    public class Shape {
        public static Shape [] shapes = new Shape[3];

        public enum Type {
            Bullet,
            Cone,
            Circle
        };

        public Type T { get; }

        private Shape(Type t) {
            this.T = t;
        }

        public static void Initialize() {
            for (int i = 0; i < shapes.Length; i++) {
                shapes[i] = new Shape((Type)i);
            }
        }
    }
}
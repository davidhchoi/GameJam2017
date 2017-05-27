namespace GameJam2017.Glamour {
    public class Shape {
        public static Shape [] shapes = new Shape[3];

        enum Type {
            Bullet,
            Cone,
            Circle
        };

        private Type t;
        private Shape(Type t) {
            this.t = t;
        }

        public static void Initialize() {
            for (int i = 0; i < shapes.Length; i++) {
                shapes[i] = new Shape((Type)i);
            }
        }
    }
}
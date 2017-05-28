using System;

namespace GameJam2017.Glamour {
    public class Shape {
        public static Shape [] shapes = new Shape[3];

        public enum Type {
            Bullet,
            Cone,
            Circle
        };

        public Type T { get; }

        public int Cost() {
            switch (T) {
                case Type.Bullet:
                    return 0;
                case Type.Cone:
                    return 1;
                case Type.Circle:
                    return 2;
            }
            throw new Exception("Something went wrong");
        }

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
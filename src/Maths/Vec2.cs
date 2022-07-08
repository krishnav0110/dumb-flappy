namespace Maths {
    public class Vec2 {
        public float x, y;

        public Vec2() {
            x = 0;
            y = 0;
        }
        public Vec2(float x, float y) {
            this.x = x;
            this.y = y;
        }

        public void zero() {
            x = 0;
            y = 0;
        }

        public void setMagnitudeAngle(float magnitude, float angle) {
            angle *= (float) Math.PI / 180f;
            x = magnitude * (float) Math.Cos(angle);
            y = magnitude * (float) Math.Sin(angle);
        }





        public static Vec2 operator+ (Vec2 a, Vec2 b) {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static Vec2 operator- (Vec2 a, Vec2 b) {
            return new Vec2(a.x - b.x, a.y - b.y);
        }

        public static Vec2 operator* (Vec2 a, float k) {
            return new Vec2(k * a.x, k * a.y);
        }

        public static Vec2 operator* (float k, Vec2 a) {
            return new Vec2(k * a.x, k * a.y);
        }

        public static Vec2 operator/ (Vec2 a, float k) {
            return new Vec2(a.x / k, a.y / k);
        }





        public float length() {
            return (float) Math.Sqrt(x * x + y * y);
        }
    }
}
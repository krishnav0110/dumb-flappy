using Maths;
using MainEngine;

namespace Objects {
    public class PipePair: IEntity {

        public static readonly float WIDTH = 50f;
        public static readonly float SPACING = 125f;

        private Vec2 position = new Vec2();

        private static Random rand = new Random();



        public Vec2 Position { get{ return position; }}





        public PipePair() {
            setInitialPosition();
        }

        private void setInitialPosition() {
            float y = rand.Next(10, Renderer.HEIGHT - (int) SPACING - 10);
            position = new Vec2(Renderer.WIDTH, y);
        }

        public void update(float dt) {
            position -= Bird.VELOCITY * dt;

            if(position.x < -WIDTH)
                setInitialPosition();
        }

        public void render(Graphics g) {
            SolidBrush brush = new SolidBrush(Color.Green);

            g.FillRectangle(brush, (int) position.x, 0, WIDTH, (int) position.y);
            g.FillRectangle(brush, (int) position.x, position.y + SPACING, WIDTH, Renderer.HEIGHT);
            
            brush.Dispose();
        }
    }
}
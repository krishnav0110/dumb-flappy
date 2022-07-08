using Maths;
using Genetic;
using NeuralNetwork;
using MainEngine;

namespace Objects {
    public class Bird: IEntity, IAiUnit {

        public static readonly float SIZE = 20f;
        public static readonly float JUMPINGPOWER = -400f;
        public static readonly Vec2 GRAVITY = new Vec2(0f, 700f);
        public static readonly Vec2 VELOCITY = new Vec2(100f, 0f);
        public static readonly Vec2 initialPosition = new Vec2(100f, 200f);


        private Vec2 position = new Vec2();
        private Vec2 velocity = new Vec2();
        private bool alive = true;


        private float[] inputs = new float[3];
        private NeuralNet brain;
        private int score = 0;
        private float fitness = 0f;





        public Vec2 Position { get{ return position; }}
        public bool Alive { get{ return alive; }}

        public NeuralNet Brain {
            get{ return brain; }
            set{
                if(brain != null)
                    brain = value;
            }
        }

        public float[] Inputs { set{inputs = value; }}
        public int Score { get{ return score; }}
        public float Fitness { get{ return fitness; } set{ fitness = value; }}





        public Bird() {
            this.position = initialPosition;
            this.brain = new NeuralNet(3, 2, 1);
        }





        public void update(float dt) {
            Matrix res = this.brain.think(inputs);
            if(res[0, 0] >= 0.5)
                this.velocity.y = JUMPINGPOWER;

            this.velocity += GRAVITY * dt;
            this.position += this.velocity * dt;
        }

        public void render(Graphics g) {
            SolidBrush brush = new SolidBrush(Color.FromArgb(50, 255, 255, 0));

            g.FillEllipse(brush, position.x, position.y, SIZE, SIZE);
            g.DrawString(score.ToString(), new Font("Arial", 10), brush, position.x, position.y - 12);

            brush.Dispose();
        }





        public void updateScore() {
            ++this.score;
        }

        public void checkCollision(PipePair pipe) {
            if(position.y < -SIZE || position.y > Renderer.HEIGHT) {
                this.alive = false;
                return;
            }

            if(position.x + SIZE < pipe.Position.x || position.x > pipe.Position.x + PipePair.WIDTH)
                return;

            if(position.y > pipe.Position.y && position.y + SIZE < pipe.Position.y + PipePair.SPACING)
                return;
            
            this.alive = false;
        }
    }
}
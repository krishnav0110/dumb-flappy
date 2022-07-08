using InputsHandler;
using NeuralNetwork;
using Genetic;
using Objects;

namespace MainEngine {
    public class Engine {
        public readonly Loop loop = new Loop();
        public readonly Renderer renderer = new Renderer();


        private Population<Bird> population = new Population<Bird>();
        private List<Bird> birds = new List<Bird>();
        private List<PipePair> pipes = new List<PipePair>(1);


        private readonly System.Timers.Timer timer = new System.Timers.Timer(2000);


        private static float speedFactor = 1f;

        public static float SpeedFactor { get { return speedFactor; }}





        public Engine() {
            this.loop.engine = this;
            this.renderer.engine = this;
            this.population.engine = this;
            this.birds = population.entities;

            timer.Elapsed += updateScores;
            timer.Enabled = true;
            setInitialPosition();
        }

        private void setInitialPosition() {
            pipes.Clear();
            pipes.Add(new PipePair());
        }





        public void update(float delta) {
            float dt = delta * speedFactor;

            if(population.shouldStartNextGeneration())
                setInitialPosition();

            foreach (IEntity entity in pipes) {
                entity.update(dt);
            }

            PipePair nextPipe = getNextPipe();

            foreach (Bird entity in birds) {
                float[] inputs = new float[3] {
                    entity.Position.x - nextPipe.Position.x,
                    entity.Position.y - nextPipe.Position.y,
                    Bird.VELOCITY.x,
                };

                entity.Inputs = inputs;
                entity.update(dt);

                entity.checkCollision(nextPipe);
            }

            population.update();
        }

        public void render(Graphics g) {
            foreach (IEntity entity in pipes) {
                entity.render(g);
            }
            foreach (IEntity entity in birds) {
                entity.render(g);
            }

            this.population.render(g);
        }





        private void updateScores(object? sender, System.Timers.ElapsedEventArgs e) {
            foreach(Bird entity in birds) {
                entity.updateScore();
            }
        }

        private PipePair getNextPipe() {
            return pipes[0];
        }





        public void handleKeyDown(Keys key) {
            KeyboardHandler.KeyPressed = key;

            switch(key) {
                case Keys.D:
                    this.toggleSpeed();
                break;
            }
        }

        private void toggleSpeed() {
            if(speedFactor == 1f)
                speedFactor = 2f;
            else
                speedFactor = 1f;
        }
    }
}
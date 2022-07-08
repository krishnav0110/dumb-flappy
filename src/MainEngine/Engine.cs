using InputsHandler;
using NeuralNetwork;
using Genetic;
using Objects;
using Logic;

namespace MainEngine {
    public class Engine {

        public readonly Loop loop = new Loop();
        public readonly Renderer renderer = new Renderer();
        public readonly Game game = new Game();


        private Population<Bird> population = new Population<Bird>();
        private List<Bird> birds = new List<Bird>();


        private static float speedFactor = 1f;

        public List<Bird> Birds { get{ return birds; }}
        public static float SpeedFactor { get { return speedFactor; }}





        public Engine() {
            this.loop.engine = this;
            this.renderer.engine = this;
            this.population.engine = this;
            this.game.engine = this;
            this.birds = population.entities;

            game.setInitialPosition();
        }





        public void update(float delta) {
            float dt = delta * speedFactor;
            
            if(population.shouldStartNextGeneration())
                game.setInitialPosition();

            game.update(dt);

            PipePair nextPipe = game.getFirstPipe();

            foreach (Bird entity in birds) {
                float[] inputs = new float[] {
                    entity.Position.x - nextPipe.Position.x + PipePair.WIDTH,
                    entity.Position.y - nextPipe.Position.y,
                    entity.Position.y - nextPipe.Position.y + PipePair.SPACING,
                };

                entity.Inputs = inputs;
                entity.update(dt);

                entity.checkCollision(nextPipe);
            }

            population.update();
        }

        public void render(Graphics g) {
            foreach (IEntity entity in birds) {
                entity.render(g);
            }

            this.game.render(g);
            this.population.render(g);
        }





        public void handleKeyDown(Keys key) {
            KeyboardHandler.KeyPressed = key;

            switch(key) {
                case Keys.F:
                    toggleSpeed();
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
using MainEngine;
using Objects;

namespace Logic {
    public class Game {
        public Engine? engine;

        private static readonly int PIPESPACING = 350;
        private static readonly int PIPESCOUNT = (int) Math.Ceiling((Renderer.WIDTH + PipePair.WIDTH) / PIPESPACING);

        private int firstPipeIndex = 0;
        private List<PipePair> pipes = new List<PipePair>(PIPESCOUNT);


        private int score = 0;
        private readonly System.Timers.Timer timer = new System.Timers.Timer(1000);





        public Game() {
            timer.Elapsed += updateScores;
            timer.Enabled = true;
        }

        public void setInitialPosition() {
            timer.Stop();

            score = 0;
            pipes.Clear();
            firstPipeIndex = 0;

            pipes.Add(new PipePair());
            for(int i = 1; i < PIPESCOUNT; ++i) {
                pipes.Add(new PipePair(pipes[0].Position.x + i * PIPESPACING));
            }

            timer.Start();
        }





        private void updateScores(object? sender, System.Timers.ElapsedEventArgs e) {
            if(engine == null)
                return;

            ++score;
            foreach(Bird entity in engine.Birds) {
                entity.updateScore();
            }
        }





        public void update(float dt) {
            foreach (PipePair pipe in pipes) {
                pipe.update(dt);
            }
        }

        public void render(Graphics g) {
            foreach (IEntity entity in pipes) {
                entity.render(g);
            }

            g.DrawString(score.ToString(), new Font("Arial", 15), new SolidBrush(Color.White), Renderer.WIDTH >> 1, 10);
        }





        public PipePair getFirstPipe() {
            if(pipes[firstPipeIndex].Position.x < 50f - PipePair.WIDTH)
                ++firstPipeIndex;

            if(firstPipeIndex >= PIPESCOUNT)
                firstPipeIndex = 0;

            return pipes[firstPipeIndex];
        }
    }
}
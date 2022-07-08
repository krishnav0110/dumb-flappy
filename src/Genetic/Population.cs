using Maths;
using Objects;
using NeuralNetwork;
using MainEngine;

namespace Genetic {
    public class Population<T> where T: IAiUnit, new() {

        public Engine? engine;
        public static readonly int size = 100;
        private int generation = 0;



        public List<T> entities;
        public List<T> deadEntities;
        private T? bestEntity;

        Random rand = new Random();





        public int Generation { get{ return generation; }}
        public T? BestEntity { get{ return bestEntity; }}





        public Population() {
            entities = new List<T>(size);
            deadEntities = new List<T>(size);

            for(int i = 0; i < size; ++i) {
                entities.Add(new T());
            }
        }



        

        public void nextGeneration() {
            ++generation;
            calcFitness();
            for(int i = 0; i < size; ++i) {
                T entity = pickOne();
                T child = new T();
                child.Brain = entity.Brain.copy();
                child.Brain.mutate(0.1f);
                entities.Add(child);
            }

            deadEntities.Clear();
        }

        private void calcFitness() {
            float sum = 0f;
            foreach (T entity in deadEntities) {
                sum += entity.Score;
            }

            foreach (T entity in deadEntities) {
                entity.Fitness = ((float) entity.Score) / sum;
            }

            float max = 0f;
            foreach (T entity in deadEntities) {
                if(entity.Fitness > max) {
                    max = entity.Fitness;
                    this.bestEntity = entity;
                }
            }
        }

        private T pickOne() {
            float r = rand.Next(0, 100) / 100f;
            float cummulative = 0f;

            foreach (T entity in deadEntities) {
                cummulative += entity.Fitness;
                if(r <= cummulative)
                    return entity;
            }
            
            return deadEntities.Last();
        }





        public void update() {
            for(int i = entities.Count - 1; i >= 0; --i) {
                T entity = entities[i];
                if(entity.Alive) 
                    continue;

                entities.RemoveAt(i);
                deadEntities.Add(entity);
            }
        }

        public void render(Graphics g) {
            Font font = new Font("Arial", 10);
            SolidBrush brush = new SolidBrush(Color.White);

            g.DrawString($"Population: {size}", font, brush, 0, 0);
            g.DrawString($"Generation: {generation}", font, brush, 0, 12);
            g.DrawString($"Count: {entities.Count}", font, brush, 0, 24);
            g.DrawString($"SpeedFactor: {Engine.SpeedFactor}", font, brush, 0, 36);

            font.Dispose();
            brush.Dispose();
        }





        public bool shouldStartNextGeneration() {
            if(entities.Count != 0)
                return false;

            nextGeneration();
            return true;
        }
    }
}
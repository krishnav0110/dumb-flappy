namespace Maths {
    public class Matrix {
        private int rows, cols;
        private float[ , ] values;





        public int Rows { get{ return rows; }}
        public int Cols { get{ return cols; }}
        public float this[int i, int j] {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }





        public Matrix(int rows, int cols) {
            this.rows = rows;
            this.cols = cols;
            this.values = new float[this.rows, this.cols];
        }

        public Matrix(float[] values) {
            this.rows = values.Length;
            this.cols = 1;
            this.values = new float[this.rows, this.cols];

            for(int i = 0; i < this.rows; ++i)
                this.values[i, 0] = values[i];
        }





        public void randomize(int bound) {
            Random rand = new Random();
            for(int i = 0; i < this.rows; ++i) {
                for(int j = 0; j < this.cols; ++j) {
                    this.values[i, j] = (float) (rand.Next(-100, 100) * bound / 100f);
                }
            }
        }

        public void mutate(float mutationChance) {
            mutationChance *= 100f;
            Random rand = new Random();
            for(int i = 0; i < rows; ++i) {
                for(int j = 0; j < cols; ++j) {
                    if(rand.Next(0, 100) < mutationChance)
                        values[i, j] = (float) (rand.Next(-100, 100) * 4f / 100f);
                }
            }
        }

        public void print() {
            for(int i = 0; i < this.rows; ++i) {
                for(int j = 0; j < this.cols; ++j) {
                    System.Console.Write(this.values[i, j] + "\t");
                }
                System.Console.WriteLine();
            }
        }

        public static Matrix operator+(Matrix a, Matrix b) {
            Matrix c = new Matrix(a.rows, a.cols);
            for(int i = 0; i < c.rows; ++i) {
                for(int j = 0; j < c.cols; ++j) {
                    c.values[i, j] = a.values[i, j] + b.values[i, j];
                }
            }
            return c;
        }

        public static Matrix operator-(Matrix a, Matrix b) {
            Matrix c = new Matrix(a.rows, a.cols);
            for(int i = 0; i < c.rows; ++i) {
                for(int j = 0; j < c.cols; ++j) {
                    c.values[i, j] = a.values[i, j] - b.values[i, j];
                }
            }
            return c;
        }

        public static Matrix operator*(Matrix a, float k) {
            Matrix c = new Matrix(a.rows, a.cols);

            for(int i = 0; i < c.rows; ++i) {
                for(int j = 0; j < c.cols; ++j)
                    c.values[i, j] = a.values[i, j] * k;
            }
            return c;
        }

        public static Matrix operator*(Matrix a, Matrix b) {
            Matrix c = new Matrix(a.rows, b.cols);

            for(int i = 0; i < c.rows; ++i) {
                for(int j = 0; j < c.cols; ++j) {
                    float sum = 0f;
                    for(int k = 0; k < a.cols; ++k)
                        sum += a.values[i, k] * b.values[k, j];

                    c.values[i, j] = sum;
                }
            }
            return c;
        }

        public void hadamardProduct(Matrix b) {
            for(int i = 0; i < this.rows; ++i) {
                for(int j = 0; j < this.cols; ++j) {
                    this.values[i, j] *= b.values[i, j];
                }
            }           
        }

        public void map(Func<float, float> f) {
            for(int i = 0; i < this.rows; ++i) {
                for(int j = 0; j < this.cols; ++j)
                    this.values[i, j] = f(this.values[i, j]);
            }
        }

        public Matrix transpose() {
            Matrix t = new Matrix(this.cols, this.rows);
            for(int i = 0; i < t.rows; ++i) {
                for(int j = 0; j < t.cols; ++j)
                    t.values[i, j] = this.values[j, i];
            }
            return t;
        }

        public Matrix copy() {
            Matrix thisCopy = new Matrix(this.rows, this.cols);
            for(int i = 0; i < this.rows; ++i) {
                for(int j = 0; j < this.cols; ++j)
                    thisCopy.values[i, j] = this.values[i, j];
            }
            return thisCopy;
        }

        public static float sigmoid(float x) {
            return (float) (1f / (1f + Math.Exp(-x)));
        }

        public static float dsigmoid(float y) {
            return (float) (y * (1f - y));
        }
    }
}
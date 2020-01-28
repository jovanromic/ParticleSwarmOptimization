using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    class ParticleSwarmOptimizationAlgorithm
    {
        public Particle[] swarm;
        public double gbestValue;
        public int[] gbestPosition;
        public int size;
        public int termination;
        public Random rnd;
        public ParticleSwarmOptimizationAlgorithm(int size, int termination)
        {
            this.size = size;
            this.gbestValue = double.MaxValue;
            this.termination = termination;
            this.gbestPosition = new int[2];
            this.rnd = new Random();
            
            swarm = new Particle[size];
            for (int i = 0;i<size;i++)
            {
                int[] p = new int[2];
                p[0] = rnd.Next(75, 101);
                p[1] = rnd.Next(3000, 14001);

                double[] d = new double[2];
                d[0] = rnd.Next(1, 6);
                d[1] = rnd.Next(1, 101);
                swarm[i] = new Particle(p, d, 2);
            }
        }

        public void Start(int Talt, int Lalt, int d)
        {
            Console.WriteLine("Particle swarm optimization algorithm started.");
            for(int j = 0;j<termination;j++)
            {
                Console.WriteLine("Iteration {0} begun.", j + 1);

                for (int i = 0; i < size; i++)
                {
                    Console.WriteLine("Particle {0}: ", i + 1);
                    double value = swarm[i].Evaluate(Talt, Lalt, d);
                    Console.WriteLine(swarm[i].ToString());

                    if (value < gbestValue)
                    {
                        gbestValue = value;
                        swarm[i].bestPosition.CopyTo(gbestPosition, 0);
                        Console.WriteLine("New best value: {0}\nPositon: {1} {2}",
                            value, gbestPosition[0], gbestPosition[1]);
                    }

                    swarm[i].Move(rnd, gbestPosition, 75, 100, 3000, 14000);
                }
            }


            Console.WriteLine("Particle swarm optimization algorithm finished.");
            Console.WriteLine("The least fuel consumed is {0} gallons at {1}% power at {2} feet.",
                gbestValue, gbestPosition[0], gbestPosition[1]);
        }
    }
}

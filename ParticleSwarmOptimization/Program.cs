using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter takeoff altitude in feet:");
            int Talt = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter landing altitude in feet:");
            int Lalt = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter flight distance in nautical miles:");
            int d = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of particles:");
            int nop = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter number of iterations:");
            int noi = int.Parse(Console.ReadLine());

            ParticleSwarmOptimizationAlgorithm algorithm =
                new ParticleSwarmOptimizationAlgorithm(nop, noi);

            algorithm.Start(Talt, Lalt, d);
            Console.ReadKey();
            //for(int i = 75;i<101;i++)
            //{
            //    Console.WriteLine(Particle.CorrectiveFactor(i).ToString());
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    class Particle
    {
        public static double w = 0.729;
        public static double cognitive = 1.49445;
        public static double social = 1.49445;

        public int[] position;
        public double[] velocity;
        public int[] bestPosition;
        public double deviation;
        public double currentValue;
        public double bestValue;

        public double r1, r2;

        public Particle(int[] p, double[] v, int dim)
        {
            position = new int[dim];
            velocity = new double[dim];
            bestPosition = new int[dim];

            p.CopyTo(position, 0);
            p.CopyTo(bestPosition, 0);
            v.CopyTo(velocity, 0);

            deviation = double.MaxValue; 
            currentValue = double.MaxValue;
            bestValue = double.MaxValue;
        }

        public override string ToString()
        {
            string res = "";
            res += "-------------------------------\nCurrent position: ";
            for(int i = 0; i< position.Length;i++)
            {
                res += position[i].ToString() + " ";
            }
            res += "\nVelocity: ";
            for(int i=0;i<velocity.Length;i++)
            {
                res += velocity[i].ToString()+" ";
            }
            res += "\nPbest value: " + bestValue.ToString();
            res += "\nCurrent value: " + currentValue.ToString();
            res += "\n----------------------------------\n";

            return res;
        }
         
        public void Move(Random rnd, int[] gbestPosition, int powerMin, int powerMax, int altMin, int altMax)
        {
            double[] v = new double[2];
            int[] p = new int[2];
            position.CopyTo(p, 0);

            double[] norm = { 1d, 0.001d };

            for (int i = 0; i < velocity.Length; i++)
            {
                r1 = rnd.NextDouble();
                r2 = rnd.NextDouble();

                v[i] = w*velocity[i] + r1 * cognitive * (bestPosition[i] - position[i])*norm[i] +
                    r2 * social * (gbestPosition[i] - position[i])*norm[i];
            }

            v.CopyTo(velocity, 0);

            for(int i = 0; i< position.Length;i++)
            {
                p[i] += (int)Math.Round(v[i]);
            }

            if (p[0] < powerMin)
                p[0] = powerMin;
            else if (p[0] > powerMax)
                p[0] = powerMax;

            if (p[1] < altMin)
                p[1] = altMin;
            else if (p[1] > altMax)
                p[1] = altMax;

            p.CopyTo(position, 0);
        }

        public double Evaluate(int Talt, int Lalt, int d)
        {
            double gph = 0;
            int alt = position[1];
            int power = position[0];

            double tcl, tcr, tde, ccr, dcr;

            tcl = (alt - Talt) / (54000*0.7);
            tde = (alt - Lalt + 1000) / 42000;
            dcr = d - 80 * tcl - 90 * tde;
            tcr = 19d / 29d * dcr / power;
            ccr = Math.Log(power) / Math.Log(alt) * 29;

            gph = (tcl * 16 + tcr * ccr + tde * 2)*CorrectiveFactor(power);// / (tcl + tcr + tde);

            if (gph < bestValue)
            {
                bestValue = gph;
                position.CopyTo(bestPosition, 0);
            }
            currentValue = gph;
            return gph;
        }

        public static double CorrectiveFactor(int power)
        {
            return (power - 75) * (1.21 - 1) / (100 - 75) + 1;
        }
    }
}

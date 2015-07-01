using System;
using System.Collections.Generic;
using System.Linq;

namespace Percolation
{
    internal class PercolationExperiment
    {
        private double[] p;
        private int size = 0;
        private int times = 0;

        public PercolationExperiment(int N, int numberOfTimes)
        {
            if (N <= 0 || numberOfTimes <= 0)
            {
                throw new ArgumentException("parameters must be positive");
            }

            times = numberOfTimes;
            size = N;
            p = new double[times];
            for (int i = 0; i < times; i++)
            {
                double count = 0.0; // number of opened site
                Percolation percolation = new Percolation(size); // create the grid ready for percolation
                Random random = new Random();
                while (!percolation.CheckIfPercolates())
                {
                    var x = random.Next(size) + 1;
                    var y = random.Next(size) + 1;
                    if (!percolation.IsOpen(x, y))
                    {
                        percolation.OpenByIndex(x, y);
                        count++;
                    }
                }

                p[i] = count / (size * size);
            }

        }
        public double GetMean()
        {
            return Mean(p);
        }

        public double GetStdDev()
        {
            if (times == 1)
            {
                return Double.NaN;
            }
            return StandardDeviation(p);
        }

        public double GetConfidenceLo()
        {
            return GetMean() - 1.96 * GetStdDev() / Math.Sqrt(times);
        }

        public double GetconfidenceHi()
        {
            return GetMean() + 1.96 * GetStdDev() / Math.Sqrt(times);
        }

        private static double StandardDeviation(IEnumerable<double> valueList)
        {
            var m = 0.0;
            var s = 0.0;
            var k = 1;
            foreach (var value in valueList)
            {
                var tmpM = m;
                m += (value - tmpM) / k;
                s += (value - tmpM) * (value - m);
                k++;
            }

            return Math.Sqrt(s / (k - 2));
        }

        private static double Mean(IEnumerable<double> values)
        {
            return !values.Any() ? 0 : Mean(values, 0, values.Count());
        }

        private static double Mean(IEnumerable<double> values, int start, int end)
        {
            double s = 0;

            for (var i = start; i < end; i++)
            {
                s += values.ElementAt(i);
            }

            return s / (end - start);
        }
    }
}
using System;

namespace Percolation
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var N = 0;
            var numberOfTimes = 0;
            N = int.Parse(args[0]);
            numberOfTimes = int.Parse(args[1]);
            var test = new PercolationExperiment(N, numberOfTimes);
            Console.WriteLine("mean = {0}", test.GetMean());
            Console.WriteLine("stddev = {0}", test.GetStdDev());
            Console.WriteLine("95% confidence interval = {0}, {1}", test.GetConfidenceLo(), test.GetconfidenceHi());
            Console.ReadLine();
        }
    }
}
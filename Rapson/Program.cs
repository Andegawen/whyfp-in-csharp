using System;

namespace Rapson
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = 567.0;
            Console.WriteLine(Square(number, number/2, 0.00001));
            Console.ReadKey();
        }

        public static double Square(double number, double startApproximation, double eps)
        {
            double nextApproximation = 0;
            double currentApproximation = 0;
            do
            {
                currentApproximation = startApproximation;
                nextApproximation = (currentApproximation + number / currentApproximation) / 2.0;
                startApproximation = nextApproximation;
            } while (Math.Abs(nextApproximation - currentApproximation) >= eps);

            return nextApproximation;
        }
    }
}

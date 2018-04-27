using System;
using System.Collections.Generic;

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
            double GetNextApproximationForNumber(double startApprox) => GetNextApproximation(number, startApprox);
            var approximations = GenerateApproximations(GetNextApproximationForNumber, startApproximation);
            return FindTheBestApproximation(eps, approximations, startApproximation);
        }

        public static double SomeFunc(double x)
        {
            return 3 * x * x;
        }

        public static double EasyDiff(Func<double, double> function, double number, double h)
        {
            return (function(number + h) - function(number)) / h;
        }

        private static double FindTheBestApproximation(double eps, IEnumerable<double> approximations, double startApproximation)
        {
            var currentApproximation = startApproximation;
            var approximationEnumerator = approximations.GetEnumerator();
            approximationEnumerator.MoveNext();
            var nextApproximation = approximationEnumerator.Current;
            while (ShouldSearchBetterApproximation(eps, nextApproximation, currentApproximation))
            {

                currentApproximation = approximationEnumerator.Current;
                approximationEnumerator.MoveNext();
                nextApproximation = approximationEnumerator.Current;
            }

            return nextApproximation;
        }

        private static IEnumerable<double> GenerateApproximations(Func<double, double> getNextIteration, double currentApproximation)
        {
            var nextApproximation = getNextIteration(currentApproximation);
            yield return nextApproximation;
            foreach (var approximation in GenerateApproximations(getNextIteration, nextApproximation))
            {
                yield return approximation;
            }
        }

        private static bool ShouldSearchBetterApproximation(double eps, double nextApproximation, double currentApproximation)
        {
            return Math.Abs(nextApproximation - currentApproximation) >= eps;
        }

        private static double GetNextApproximation(double number, double currentApproximation)
        {
            return (currentApproximation + number / currentApproximation) / 2.0;
        }
    }
}

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
            var approximations = GenerateApproximations(number, GetNextApproximation, startApproximation);
            return FindTheBestApproximation(eps, approximations, startApproximation);
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

        private static IEnumerable<double> GenerateApproximations(double number, Func<double, double, double> getNextIteration, double currentApproximation)
        {
            var nextApproximation = getNextIteration(number, currentApproximation);
            yield return nextApproximation;
            foreach (var approximation in GenerateApproximations(number, getNextIteration, nextApproximation))
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

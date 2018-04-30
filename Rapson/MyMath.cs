using System;
using System.Collections.Generic;

namespace Rapson
{
    public static class MyMath
    {
        public static double Within(double eps, IEnumerable<double> approximations)
        {
            var approximationEnumerator = approximations.GetEnumerator();
            approximationEnumerator.MoveNext();
            var currentApproximation = approximationEnumerator.Current;
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

        public static IEnumerable<double> Repeat(Func<double, double> getNextIteration, double currentApproximation)
        {
            var nextApproximation = getNextIteration(currentApproximation);
            yield return nextApproximation;
            foreach (var approximation in Repeat(getNextIteration, nextApproximation))
            {
                yield return approximation;
            }
        }

        public static bool ShouldSearchBetterApproximation(double eps, double nextApproximation, double currentApproximation)
        {
            return Math.Abs(nextApproximation - currentApproximation) >= eps;
        }
    }
}

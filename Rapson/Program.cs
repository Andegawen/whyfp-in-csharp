using System;
using System.Collections.Generic;
using System.Linq;

namespace Rapson
{
    class Program
    {
        static void Main(string[] args)
        {
            var number = 567.0;
            Console.WriteLine(Square(number, startApproximation: number/2, eps:0.00001));
            Console.WriteLine(Derivative(SomeFunc, number: 2, h: 1, eps:0.0001));
            Console.ReadKey();
        }

        public static double Square(double number, double startApproximation, double eps)
        {
            double NextApproximation(double startApprox) => GetNextApproximation(number, startApprox);
            return MyMath.Within(eps, MyMath.Repeat(NextApproximation, startApproximation));
        }

        public static double Derivative(Func<double, double> function, double number, double h, double eps)
        {
            return MyMath.Within(eps, DerivativeApproximations(h, function, number));
        }

        public static double SomeFunc(double x)
        {
            return 3 * x * x;
        }

        public static double EasyDiff(Func<double, double> function, double number, double h)
        {
            return (function(number + h) - function(number)) / h;
        }

        public static double Halve(double x)
        {
            return x / 2;
        }

        public static IEnumerable<double> DerivativeApproximations(double h0, Func<double, double> function, double number)
        {
            return MyMath.Repeat(Halve, h0).Select(h => EasyDiff(function, number, h));
        }

        private static double GetNextApproximation(double number, double currentApproximation)
        {
            return (currentApproximation + number / currentApproximation) / 2.0;
        }
    }
}

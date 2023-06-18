using System;
using static System.Math;
using static System.Console;

class main {

	public static void Main() {
		int count = 0;
		Func<double, double, double> f = (x,y) => { count++; return 42*y*y-12*x;  };
		Func<double, double> d = x => { return Pow(x-2,2); };
		Func<double, double> u = x => { return 6; };
		double a = 0;
		double b = 4;

		(double i, double e) = cubature.integ2D(f, a, b, d, u, 1e-4, 1e-4);
		WriteLine($"{i}	{e}	{count}");
	}

} // End class

using System;
using static System.Console;
using static System.Math;

class main {
	
	static void Main() {
		Func<double, double> f;

		/* Integral of sqrt(x) from 0 to 1 */
		f = delegate(double x) { return Sqrt(x); };
		double I1val = integrator.integrate(f, 0, 1);
		WriteLine($"{I1val}");
	
	}
}

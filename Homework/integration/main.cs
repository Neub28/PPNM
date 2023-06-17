using System;
using System.IO;
using static System.Console;
using static System.Math;

class main {
	
	static void Main() {
		Func<double, double> f;

		/* Integral of sqrt(x) from 0 to 1 */
		f = delegate(double x) { return Sqrt(x); };
		(double I1val, double i1err) = integrator.integrate(f, 0, 1);
		WriteLine($"1) x-> [0,1] f(x) = sqrt(x):\n2/3 = {I1val}	{integrator.approx(I1val, 2.0/3.0)}");
		WriteLine($"± {i1err}");
		WriteLine("");

		/* Integral of sqrt(x)⁻1 from 0 to 1  */	
		f = delegate(double x) {return 1.0/Sqrt(x);};
		(double I2val, double i2err) = integrator.integrate(f, 0, 1);
		WriteLine($"2) x-> [0,1] f(x) = 1/sqrt(x):\n2 = {I2val}	{integrator.approx(I2val, 2.0)}");
		WriteLine($"± {i2err}");
		WriteLine("");

		/* Integral of 4sqrt(1-x²) from 0 to 1 */
		f = delegate(double x) {return 4.0*Sqrt(1-Pow(x,2)); };
		(double I3val, double i3err) = integrator.integrate(f, 0, 1);
		WriteLine($"3) x-> [0,1] f(x) = 4sqrt(1-x²):\n{PI} = {I3val}	{integrator.approx(I3val, PI)}");
		WriteLine($"± {i3err}");
		WriteLine("");

		/* Integral of ln(x)/sqrt(x) from 0 to 1  */
		f = delegate(double x) {return Log(x)/Sqrt(x); };
		(double I4val, double i4err) = integrator.integrate(f, 0, 1);
		WriteLine($"4) x-> [0,1] f(x) = ln(x)/sqrt(x):\n-4 = {I4val}	{integrator.approx(I4val, -4)}");
		WriteLine($"± {i4err}");
		WriteLine("");

		/* Evalute erf(z) from -2 to 2 */
		var outfile = new StreamWriter("erf.txt");
		for(double x = -2.0; x <= 2.0; x += 1.0/16) {
			outfile.WriteLine($"{x}	{integrator.erf(x)}");
		}
		outfile.Close();
		WriteLine("A plot of tabulated values and integrated values of the error-function is in the file erf.svg");
		/* Testing implementation of Clenshaw-Curtis */
	
		WriteLine("");
		/* Integral of 1/sqrt(x) from 0 to 1 */
		int count = 0;
		f = delegate(double x) { count++; return 1.0/Sqrt(x); };
		(double ccI1, double cci1err) = integrator.ccintegrate(f, 0, 1);
		WriteLine($"Clenshaw-Curtis: x-> [0,1] f(x)=1/sqrt(x):\n{ccI1} = {2}	{integrator.approx(ccI1, 2.0)} with {count} evaluations");
		WriteLine($"± {cci1err}");
		WriteLine("");

		/* Integral of ln(x)/sqrt(x) from 0 to 1 */
		int count2 = 0;
		f = delegate(double x) { count2++; return Log(x)/Sqrt(x); };
		(double ccI2, double cci2err) = integrator.ccintegrate(f, 0, 1);
		WriteLine($"Clenshaw-Curtis: x-> [0, 1] f(x) = ln(x)/sqrt(x):\n{ccI2} = {-4}	{integrator.approx(ccI2, -4)} with {count2} evaluations");
		WriteLine($"± {cci2err}");
		WriteLine("");	
		WriteLine("Python completed these integrals in 231 and 315 iterations");
		WriteLine("");

		WriteLine("Tests for integrals containing infinite bounds");
		WriteLine("");
		count2 = 0;
		f = x => { count2++; return 1.0/Pow(x,2); };
		(double y, double err) = integrator.integrate(f, 1, double.PositiveInfinity);
		WriteLine("f(x) = 1/x²: x -> [1, ∞]");
		WriteLine($"{y} = {1.0}	{integrator.approx(y, 1.0)}	with {count2} evaluations");
		WriteLine($"± {err} ");
		WriteLine("");

		count2 = 0;
		f = x => { count2++; return x*Exp(-x*x); };
		(y, err) = integrator.integrate(f, double.NegativeInfinity, 0);
		WriteLine("f(x) = x*exp(-x²): x -> [-∞, 0]");
		WriteLine($"{y} = {-0.5}	{integrator.approx(y, -0.5)}	with {count2} evaluations");
		WriteLine($"± {err} ");
		WriteLine("");

		count2 = 0;
		f = x => { count2++; return 1/(1+x*x); };
		(y, err) = integrator.integrate(f, double.NegativeInfinity, double.PositiveInfinity);
		WriteLine("f(x) = 1/(1+x²): x -> [-∞,∞]");
		WriteLine($"{y} = {PI}	{integrator.approx(y, PI)}	with {count2} evaluations");
		WriteLine($"± {err} ");
		WriteLine("");
		WriteLine("Python completed these integrals in 15, 75 and 30 iterations.");


		}
}

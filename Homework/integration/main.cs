using System;
using static System.IO;
using static System.Console;
using static System.Math;

class main {
	
	static void Main() {
		Func<double, double> f;

		/* Integral of sqrt(x) from 0 to 1 */
		f = delegate(double x) { return Sqrt(x); };
		double I1val = integrator.integrate(f, 0, 1);
		WriteLine($"1) x-> [0,1] f(x) = sqrt(x): 2/3 = {I1val}: {integrator.approx(I1val, 2.0/3.0)}");
		
		/* Integral of sqrt(x)⁻1 from 0 to 1  */	
		f = delegate(double x) {return 1.0/Sqrt(x);};
		double I2val = integrator.integrate(f, 0, 1);
		WriteLine($"2) x-> [0,1] f(x) = 1/sqrt(x): 2 = {I2val}: {integrator.approx(I2val, 2.0)}");

		/* Integral of 4sqrt(1-x²) from 0 to 1 */
		f = delegate(double x) {return 4.0*Sqrt(1-Pow(x,2)); };
		double I3val = integrator.integrate(f, 0, 1);
		WriteLine($"3) x-> [0,1] f(x) = 4sqrt(1-x²): {PI} = {I3val}: {integrator.approx(I3val, PI)}");

		/* Integral of ln(x)/sqrt(x) from 0 to 1  */
		f = delegate(double x) {return Log(x)/Sqrt(x); };
		double I4val = integrator.integrate(f, 0, 1);
		WriteLine($"4) x-> [0,1] f(x) = ln(x)/sqrt(x): -4 = {I4val}: {integrator.approx(I4val, -4)}");

		/* Evalute erf(z) from -2 to 2 */
		var outfile = new StreamWriter("erf.txt");
		for(double x = -2.0; x >= 2.0; x += 1.0/8) {
			outfile.WriteLine($"{x}	{integrator.erf(x)}");
		}


		}
}

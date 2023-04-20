using System;
using System.IO;
using static System.Console;
using static System.Math;

class main {
	public static Func<vector, double> f;
	
	static void Main() {
	partA();
	partB();

	}
	static void partA() {
	WriteLine("---------------------- Part A --------------------");
	/* I need to test my plain monte carlo integrator */

	f = delegate(vector x) {
		if(x.norm() <= 1) return 1;
		else return 0;
	};
	vector a = new vector(0.0, 0.0);
	vector b = new vector(1.0, 1.0);
	double exact = PI/4;

	int[] nlist = new int[] {100, 250, 500, 750, 1000, 1250, 1500, 1750, 2000, 2500, 3250, 5000, 6250, 7500, 8250, 10000};

	var out1 = new StreamWriter("unitcirc.txt");

	for(int i = 0; i < nlist.Length; i++) {
		(double integral, double err) = mcint.plainmc(f, a, b, nlist[i]);
		out1.WriteLine($"{nlist[i]}	{integral}	{err}	{Abs(integral-exact)}");

	}
	out1.Close();
	WriteLine("A plot of the estimated error and actual error as functions of samplings points are in plainmc.svg.");

	/* Trying to calculate the integral suggested  */
	f = delegate(vector v) {
		return Pow(PI, -3)*Pow(1-Cos(v[0])*Cos(v[1])*Cos(v[2]), -1);
	};
	(double intg, double er) = mcint.plainmc(f, 
						new vector(0.0, 0.0, 0.0), 
						new vector(PI, PI, PI), 100000);
	WriteLine("Integral from 0 to pi of function 1/(pi³)[1-cos(x)cos(y)cos(z)]⁻1 dxdydz");
	WriteLine($"Integral is equal to:	{intg} ± {er}.");
	WriteLine($"The expcted result is:	1.3932039296856768591842462603255");
	WriteLine($"The actual error is:	{intg-1.3932039296856768591842462603255}");

	}

	static void partB() {
	WriteLine("---------------------- Part B --------------------");
	
	WriteLine("I want to test my implemented Quasi-random Monte-Carlo integrator.");
	WriteLine("I want to integrate f(r,θ,φ)=16*r*cos(θ) over the upper half of a sphere with radius 1.");
	WriteLine("The limits are r: [0:1], φ: [0: π/2], θ: [0: 2π]");
	f = delegate(vector v) { return 16*Pow(v[0],3)*Cos(v[1])*Sin(v[1]);  };
	vector aB = new vector(0.0, 0.0, 0.0);
	vector bB = new vector(1.0, PI/2.0, 2.0*PI);
	var outQ = new StreamWriter("qintg.txt");
	var outP = new StreamWriter("pintg.txt");

	int upperlim = 11000;
	int step = 1000;
	for(int N = 1000; N < upperlim; N += 1000) {
		(double qintg, double qerr) = mcint.qrand(f, aB, bB, N);
		(double pintg, double perr) = mcint.plainmc(f, aB, bB, N);

		outQ.WriteLine($"{N}	{qintg}	{qerr}");
		outP.WriteLine($"{N}	{pintg}	{perr}");

		if(N == (upperlim - step)) {
			WriteLine($"Results for N = {upperlim- step}");
			WriteLine($"Analytical result:		{4*PI}");
			WriteLine($"Quasi-random estimate:	{qintg} ± {qerr}");
			WriteLine($"Plain Monte-Carlo estimate:	{pintg} ± {perr}");
			WriteLine($"The abs. difference is:	{Abs(qintg-pintg)}");
			}

		}
	outQ.Close();
	outP.Close();

	}

}

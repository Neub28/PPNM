using System;
using System.IO;
using static System.Console;
using static System.Math;

class main {

	static void Main() {
	partA();

	}
	static void partA() {
	/* I need to test my plain monte carlo integrator */

	Func<vector, double> f = delegate(vector x) {
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

	}

}

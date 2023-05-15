using System;
using static System.Math;
using static System.Console;
using System.IO;

class main {
	public static vector x;
	public static vector y;
	public static Func<double, double> f;

	static void Main() {
		ann AI = new ann(3);
		WriteLine($"Starting parameters for ANN: ");
		for(int i = 0; i < AI.p.size; i++) {
			Write($"{AI.p[i]}\t");
			if(i%3 == 0 && i != 0) WriteLine("");
		}

		f = delegate(double x) {
			return Cos(5*x-1)*Exp(-x*x);
		};
		int size = 200;
		x = new vector(size);
		y = new vector(size);
		
		for(int i = -100; i < 100; i++) {
			x[i+100] = (double) i/100;
			y[i+100] = f(x[i+100]);
		}
		AI.train(x, y);
		
		var annout = new StreamWriter("annvalues.txt");
		for(int i = 0; i < x.size; i++) {
			annout.WriteLine($"{x[i]}	{AI.response(x[i])}");
		}
		annout.Close();

		WriteLine("\nFinal parameters:");
		for(int i = 0; i < AI.p.size; i++) {
			Write($"{AI.p[i]}   ");
			if(i%3 == 0 && i != 0) WriteLine("");
		}

	}
}

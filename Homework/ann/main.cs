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
			WriteLine($"{AI.p[i]}");
		}

		f = delegate(double x) {
			return Cos(5*x-1)*Exp(-x*x);
		};
		x = new vector(200);
		y = new vector(200);

		for(int i = -100; i < 100; i++) {
			double val = (double) i/100.0;
			x[i+100]= val;
			y[i+100]=f(val);
		}
		Error.WriteLine("Training ANN...");
		for(int i = 0; i < 2; i++) {
			AI.train(x, y);
		}
		
		var annout = new StreamWriter("annvalues.txt");
		for(int i = 0; i < x.size; i++) {
			annout.WriteLine($"{x[i]}	{AI.response(x[i])}");
		}
		annout.Close();
		

	}
}

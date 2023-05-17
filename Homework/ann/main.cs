using System;
using static System.Math;
using static System.Console;
using System.IO;

class main {
	public static vector x;
	public static vector y;
	public static Func<double, double> f;

	static void Main() {
		findParams();

	}
	static void makexy() {
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
	}

	static void partA() {
		WriteLine("-------------- PART A ------------------");
		WriteLine("I have implemented an artifical neural network.\t The following is the starting and final parameters given to and by the minimisation routine.\t The used routine is Quasi-Newton. ");
		WriteLine("By trial and error I have some starting parameters, which\t produce a satisfactory result:");
		double[] ps = new double[] {0.253109157901774, 0.118393468027186, -0.450484436447958, -0.139714191965626, 0.32423518217366, 0.491238278332743, -0.49683256912829, -0.4731172560589, 0.35301566862176};
		ann AI = new ann(3, ps);
		makexy();
		AI.train(x, y);
		WriteLine("\nFinal parameters:");
		for(int i = 0; i < AI.p.size; i++) {
			Write($"{AI.p[i]}   ");
			if((i+1)%3 == 0) WriteLine("");
		}
		printVals(AI);

	}
	static void findParams() {
		ann AI = new ann(3);
		WriteLine($"Starting parameters for ANN: ");
		for(int i = 0; i < AI.p.size; i++) {
			Write($"{AI.p[i]}\t");
			if((i+1)%3 == 0) WriteLine("");
		}

		makexy();
		AI.train(x, y);
		
		printVals(AI);
		WriteLine("\nFinal parameters:");
		for(int i = 0; i < AI.p.size; i++) {
			Write($"{AI.p[i]}   ");
			if((i+1)%3 == 0) WriteLine("");
		}

	}
	static void printVals(ann A) {
		var annout = new StreamWriter("annvalues.txt");
		for(int i = 0; i < x.size; i++) {
			annout.WriteLine($"{x[i]}	{A.response(x[i])}");
		}
		annout.Close();

	}
}

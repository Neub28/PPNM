using System;
using static System.Math;
using static System.Console;
using System.IO;

class main {
	public static double[] x;
	public static double[] y;
	public static Func<double, double> f;

	static void Main() {
		partA();

	}

	static void partA() {
		WriteLine("-------------- PART A ------------------");
		WriteLine("I have implemented an artifical neural network,\twhich can use either Quasi-Newton- or simplex-minimisation.");
		WriteLine("I will test both of these implementations.");
		f = delegate(double x) {
			return Cos(5*x-1)*Exp(-x*x);
		};
		int size = 20;
		x = new double[size];
		y = new double[size];
		double a = -1;
		double b = 1;
		
		var outpoints = new StreamWriter("trainingpoints.txt");
		for(int i = 0; i < size; i++) {
			x[i] = a + (b-a)*i/(size-1);
			y[i] = f(x[i]);
			outpoints.WriteLine($"{x[i]}	{y[i]}");
		}
		outpoints.Close();

		ann annQN = new ann(3);
		ann annSI = new ann(3);
		
		Error.WriteLine("Quasi-Newton training...");
		annQN.train(x,y, method:"qnewton", precision:1e-3);
		Error.WriteLine("Simplex training...");
		annSI.train(x,y, method:"simplex", precision:1e-6, step:0.7);
		
		WriteLine("\nFinal parameters: Quasi-Newton");
		for(int i = 0; i < annQN.p.size; i++) {
			Write($"{annQN.p[i]}   ");
			if((i+1)%3 == 0) WriteLine("");
		}
		WriteLine("\nFinal parameters: Simplex");
		for(int i = 0; i < annSI.p.size; i++) {
			Write($"{annSI.p[i]}   ");
			if((i+1)%3 == 0) WriteLine("");
		}
		WriteLine("The plots of the trained networks are in plotA.svg.");
		WriteLine("The simplex method seems to be much more robust and stable than the qnewton.");
		WriteLine("Whilst the qnewton method is very sensitive to precision the simplex simply \nalways returns something reasonable.");
		WriteLine("Both networks are however very nice compared to the analytical function, and seems \n to be in fine agreement with the parameters.");
			
		
		var outQN = new StreamWriter("annvalues.txt");
		var outSI = new StreamWriter("simvalues.txt");
		for(int i = 0; i < x.Length; i++) {
			outQN.WriteLine($"{x[i]}	{annQN.response(x[i])}");
			outSI.WriteLine($"{x[i]}	{annSI.response(x[i])}");
		}
		outQN.Close();
		outSI.Close();

		//printVals(annQN, "annvalues.txt");
		//printVals(annSI, "simvalues.txt");

	}
	static void findParams() {
		//ann AI = new ann(3);
		//WriteLine($"Starting parameters for ANN: ");
		//for(int i = 0; i < AI.p.size; i++) {
		//	Write($"{AI.p[i]}\t");
		//	if((i+1)%3 == 0) WriteLine("");
		//}
		//	
		//makexy();
		//AI.train(x, y);
		//
		//printVals(AI);
		//WriteLine("\nFinal parameters:");
		//for(int i = 0; i < AI.p.size; i++) {
		//	Write($"{AI.p[i]}   ");
		//	if((i+1)%3 == 0) WriteLine("");
		//}

	}
	static void printVals(ann A, string s) {
		var annout = new StreamWriter(s);
		for(int i = 0; i < x.Length; i++) {
			annout.WriteLine($"{x[i]}	{A.response(x[i])}");
		}
		annout.Close();

	}
}

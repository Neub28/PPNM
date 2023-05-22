using System;
using static System.Math;
using static System.Console;
using System.IO;

class main {
	public static double[] x;
	public static double[] y;
	public static Func<double, double> f;
	public static Func<double, double> fD;
	public static Func<double, double> fDD;
	public static Func<double, double> fAD;

	static void Main() {
		//partA();
		//partB();
		partC();

	}

	static void partA() {
		WriteLine("-------------- PART A ------------------");
		WriteLine("I have implemented an artifical neural network,\twhich can use either Quasi-Newton- or simplex-minimisation.");
		WriteLine("I will test both of these implementations.");
		f = delegate(double x) {
			return Cos(5*x-1)*Exp(-x*x);
		};
		int size = 35;
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
		annSI.train(x,y, method:"simplex", precision:1e-3, step:0.7);
		
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
		
	}

	static void partB() {
		WriteLine("-------------- PART B ------------------");
		WriteLine("I have modified the artifical neural network to now be able to evalute derivatives and antiderivatives.");
		WriteLine("The plot showing this is in plotB.svg.");
		WriteLine("The network does a fine job mimicing the analytical functions.");

		f = x => Cos(x);
		fD = x => -Sin(x);
		fDD = x => -Cos(x);
		fAD = x => Sin(x);

		int size = 60;
		x = new double[size];
		y = new double[size];
		double a = -PI;
		double b = PI;
		
		var outpoints = new StreamWriter("trainingpointsB.txt");
		for(int i = 0; i < size; i++) {
			x[i] = a + (b-a)*i/(size-1);
			y[i] = f(x[i]);
			outpoints.WriteLine($"{x[i]}	{y[i]}");
		}
		outpoints.Close();

		ann annB = new ann(3);
		annB.train(x,y, method:"simplex", precision:1e-3, step:0.7);
		
		var outderivs = new StreamWriter("derivpoints.txt");
		var outannderivs = new StreamWriter("annderivs.txt");
		for(int i = 0; i < 100; i++) {
			double xc = a+(b-a)*i/(100-1);
			double y0 = f(xc);
			double yD = fD(xc);
			double yDD = fDD(xc);
			double yAD = fAD(xc);
			outderivs.WriteLine($"{xc}	{y0}	{yD}	{yDD}	{yAD-fAD(0)}");
			outannderivs.WriteLine($"{xc}	{annB.response(xc)}	{annB.derivativeResponse(xc)}	{annB.dderivativeResponse(xc)}	{annB.antiderivativeResponse(xc)-annB.antiderivativeResponse(0)}");
		}
		outderivs.Close();
		outannderivs.Close();

	}

	static void partC() {		
		WriteLine("-------------- PART C ------------------");
		WriteLine("I have implemented a the neural network, which can approximate a ");
		WriteLine("solution to a differential equation by unsupervised training.");
		WriteLine("Since the harmonic oscillator is fairly simple and well-known");
		WriteLine("I will test it with such an equation.");
		WriteLine("So: φ(t)'' = -φ(t) with φ(0)=1 and φ'(0)=0");
		
		/* Vector input is phis = [ φ'', φ', φ, t  ] */
		Func<vector, double> diffeq = phis => phis[0]+phis[2]; 
		double a = 0;
		double b = 2*PI;
		double x0 = 0;
		double y0 = 1;
		double y0p = 0;

		ann neuralnetwork = new ann(3);
		neuralnetwork.diffeqTrain(diffeq, a, b, x0, y0, y0p, alpha:1, beta:1, precision:1e-3, step:0.3);

		var outdiff = new StreamWriter("diffeq.txt");
		int points = 150;
		for(int i = 0; i < points; i++) {
			double xc = a+(b-a)*i/(points-1);
			outdiff.WriteLine($"{xc}	{neuralnetwork.response(xc)}");
		}
	}

	static void printVals(ann A, string s) {
		var annout = new StreamWriter(s);
		for(int i = 0; i < x.Length; i++) {
			annout.WriteLine($"{x[i]}	{A.response(x[i])}");
		}
		annout.Close();

	}
}

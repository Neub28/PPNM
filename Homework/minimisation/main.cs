using static System.Math;
using static System.Console;
using System.IO;
using System;


class main {
	public static Func<vector, double> f;
	public static Func<vector, double> D;
	public static vector start;
	public static double ACC = 1e-9;
	public static double QNACC = 1e-2;
	public static vector res;
	public static int count;

	static void Main() {
		partA();
		partB();
	}
	static void startres(vector s, vector r, int c) {	
		WriteLine($"Start value:		({s[0]}, {s[1]})");
		WriteLine($"Numerical result:	({r[0]},{r[1]}) 	with {c} counts");

	}

	static void partA() {
		WriteLine("----------------------------------- Part A ---------------------");
		WriteLine("I am now testing my Quasi-Newton minimisation routine.");
		WriteLine("Rosenbrock's valley function: f(x,y) = (1-x)²+100(y-x²)²");
		f = delegate(vector r) {
			return (double) Pow(1-r[0],2)+100*Pow(r[1]-Pow(r[0],2),2);
		};
		start = new vector(-2, -2);
		(res, count) = minimisation.qnewton(f, start, ACC);
		
		startres(start, res, count);
		WriteLine($"Analytical result:	(1,1)");
		WriteLine($"Test:			{vector.approx(res, new vector(1.0,1.0), QNACC, QNACC) ? "PASSED" : "FAILED"}");
		WriteLine("Himmelblau's function: f(x,y) = (x²+y-11)²+(x+y²-7)²");
		f = delegate(vector r) {
			return (double) Pow(Pow(r[0],2)+r[1]-11, 2)+Pow(r[0]+Pow(r[1],2)-7,2);
		};
		start = new vector(3.5,2.5);
		(res, count) = minimisation.qnewton(f, start, ACC);
		startres(start, res, count);
		WriteLine($"Analytical result:	(3,2)");
		WriteLine($"Test:			{vector.approx(res, new vector(3.0,2.0), QNACC, QNACC) ? "PASSED" : "FAILED"}");
		start = new vector(-3, 3);
		(res, count) = minimisation.qnewton(f, start, ACC);
		startres(start, res, count);
		WriteLine($"Analytical result:	(-2.805118, 3.131312)");
		WriteLine($"Test:			{vector.approx(res, new vector(-2.805118, 3.131312), QNACC, QNACC) ? "PASSED" : "FAILED"}");
		start = new vector(-4.0, -4.0);
		(res, count) = minimisation.qnewton(f, start, ACC);
		startres(start, res, count);
		WriteLine($"Analytical result:	(-3.779310, -3.283186)");
		WriteLine($"Test:			{vector.approx(res, new vector(-3.779310, -3.283186), QNACC, QNACC) ? "PASSED" : "FAILED"}");
		start = new vector(4, -2);
		(res, count) = minimisation.qnewton(f, start, ACC);
		startres(start,res,count);
		WriteLine($"Analytical result:	(3.584428, -1.848126)");
		WriteLine($"Test:			{vector.approx(res, new vector(3.584428, -1.848126), QNACC, QNACC) ? "PASSED" : "FAILED"}");

	}

	static void partB() {
		WriteLine("----------------------------------- Part B ---------------------");
		var energy = new genlist<double>();
		var signal = new genlist<double>();
		var error = new genlist<double>();
		var seperators = new char[] {' ','\t'};
		var options = StringSplitOptions.RemoveEmptyEntries;

		do {
			string line = In.ReadLine();
			if(line == null) break;
			string[] words = line.Split(seperators, options);
			/* Skip header... */
			if(words[0] != "#") {
			energy.add(double.Parse(words[0]));
			signal.add(double.Parse(words[1]));
			error.add(double.Parse(words[2]));
			}
		}while(true);

		/* Define Breit-Weigner function: r[0] = E, r[1] = m, r[2] = Γ and r[3] = A  */
		f = delegate(vector r) {
			double E = r[0];
			double m = r[1];
			double G = r[2];
			double A = r[3];
			return A/(Pow(E-m,2)+Pow(G,2)/4);
		};
		/* Define deviation function: param[0] = m, param[1] = Γ and param[2] = A  */
		D = delegate(vector param) {
			double m = param[0];
			double G = param[1];
			double A = param[2];
			double sum = 0;
			for(int i = 0; i < energy.size; i++) {
				vector parms = new vector(energy[i], m, G, A);
				double numerator = f(parms)-signal[i];
				sum += Pow(numerator/energy[i], 2);
			}
			return sum;
		};
		start = new vector(124, 1, 7);

		(res, count) = minimisation.qnewton(D, start, 1e-4);
		WriteLine($"Parameters:	m = {res[0]} GeV");
		WriteLine($"		Γ = {res[1]} ??");
		WriteLine($"		A = {res[2]} ??");
		WriteLine($"with 		{count} tries.");
		
		var outfit = new StreamWriter("bwfit.txt");
		for(double i = 101; i < (double) 160; i += 0.1) {
			vector input = new vector(i, res[0], res[1], res[2]);
			outfit.WriteLine($"{i}	{f(input)}");
		}	

		outfit.Close();
		WriteLine("A plot of the fit and data is in file: higgsfit.svg");

	}

}

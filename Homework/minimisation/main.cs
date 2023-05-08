using static System.Math;
using static System.Console;
using System;


class main {
	public static Func<vector, double> f;
	public static vector start;
	public static double ACC = 1e-9;
	public static double QNACC = 1e-2;
	public static vector res;
	public static int count;

	static void Main() {
		partA();
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


}

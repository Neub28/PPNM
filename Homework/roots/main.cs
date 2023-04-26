using System;
using static System.Math;
using System.IO;
using static System.Console;

class main{
	public static Func<vector, vector> f;

	static void Main() {
	partA();
	partB();

	}

	static void partA() {
	/* Rosenbrock's valley function: f(x,y) = (1-x)²+100(y-x²)²  */
	/* Function is: (dfdx, dfdy) = (-2(1-x)-400x(y-x²), 200(y-x²) ) */
	f = delegate(vector x) { return new vector(-2*(1-x[0])-400*x[0]*(x[1]-x[0]*x[0]), 200*(x[1]-x[0]*x[0])); };
	vector rvroot = roots.newton(f, new vector(0.0, 0.0));
	f = delegate(vector x) { return new vector(x[0]*x[0]-9, x[1]*x[1]-9); };
	vector tdroot1 = roots.newton(f, new vector(-5.0, -5.0));
	vector tdroot2 = roots.newton(f, new vector(1.0, 1.0));
	f = delegate(vector x) { return x + new vector(5.0); };
	vector odroot = roots.newton(f, new vector(-7.0));

	WriteLine($"--------------------- Part A  --------------------------");
	WriteLine($"f(x,y) = (x²-9, y²-9): (x,y) = ({tdroot1[0]},{tdroot1[1]}) or ({tdroot2[0]},{tdroot2[1]})");
	WriteLine($"Test: {vector.approx(tdroot1, new vector(-3.0, -3.0), 1e-4, 1e-4) ? "PASSED" : "FAILED"} and {vector.approx(tdroot2, new vector(3.0, 3.0), 1e-4, 1e-4) ? "PASSED" : "FAILED"}");
	WriteLine($"f(x) = x+5: (x) = ({odroot[0]})");
	WriteLine($"Test: {vector.approx(odroot, new vector (-5.0), 1e-4, 1e-4) ? "PASSED" : "FAILED" }");
	WriteLine($"Rosenbrock: (x,y) = ({rvroot[0]}, {rvroot[1]})");
	WriteLine($"Test: {vector.approx(rvroot, new vector(1.0, 1.0), 1e-4, 1e-4) ? "PASSED" : "FAILED"}");

	}
	
	static void partB() {
	WriteLine($"--------------------- Part B  --------------------------");
	/* "Soon to be" generic lists supposed to record path of ODE solver 
	/* They are not initialized yet to only capture last run...          */
	genlist<double> xs = null;
	genlist<vector> ys = null;
	double rmin; double rmax; double acc; double eps;

	/* Function which takes an energy, uses Runge-Kutta to solve differential equation, which 
	/* is to be called as the function in the root-finding routine.   */
	rmin = 0.1;
	rmax = 8.0;
	acc = 0.01;
	eps = 0.01;

	Func<vector, vector> fB = delegate(vector EV) {
		double E = EV[0];
		Func<double, vector, vector> fD = delegate(double r, vector y) {
			return new vector(y[1], -2*(1/r+E)*y[0]);
		};
		//double rmin = 0.1;
		//double rmax = 8.0;
		vector initcon = new vector(rmin-rmin*rmin, 1-2*rmin);
		vector ods = ode.driver(fD, rmin, initcon, rmax, xs, ys);
		return new vector(ods[0]);

	};
	vector result = roots.newton(fB, new vector(-1.0));
	WriteLine($"The found root is: E0 = {result[0]}");
	WriteLine("Whereas the expected result is -0.5.");
	/* Record path of final result */
	xs = new genlist<double>();
	ys = new genlist<vector>();
	
	vector odsfinal = fB(new vector(result[0]));

	var out1 = new StreamWriter("wavefunc.txt");
	for(int i = 0; i < xs.size; i++) {
		out1.WriteLine($"{xs[i]}	{ys[i][0]}");
	}
	out1.Close();
	
	WriteLine("In the file convergence.svg I have plotted different values of \n rmax, rmin, acc and eps with their lowest energy roots. \n Note that rerunning main.cs takes quite a while. ");

	var outrmin = new StreamWriter("rmin.txt");
	for(int i = 1; i < 6; i++) {
		rmin = (double) i/10.0;
		vector result_rmin = roots.newton(fB, new vector(-0.8));
		outrmin.WriteLine($"{rmin}	{result_rmin[0]}");
	}
	outrmin.Close();

	rmin = 0.1;
	var outrmax = new StreamWriter("rmax.txt");
	for(int i = 1; i < 6; i++) {
		rmax = i;
		vector result_rmax = roots.newton(fB, new vector(-0.8));
		outrmax.WriteLine($"{rmax}	{result_rmax[0]}");
	}
	outrmax.Close();
	
	var outeps = new StreamWriter("eps.txt");
	for(int i = 1; i < 6; i++) {
		eps = (double) i/100.0;
		vector result_eps = roots.newton(fB, new vector(-0.8));
		outeps.WriteLine($"{eps}	{result_eps[0]}");
	}
	outeps.Close();
	
	eps = 0.01;
	var outacc = new StreamWriter("acc.txt");
	for(int i = 1; i < 6; i++) {
		acc = (double) i/100.0;
		vector result_acc = roots.newton(fB, new vector(-0.8));
		outacc.WriteLine($"{acc}	{result_acc[0]}");
	}
	outacc.Close();

	}
}


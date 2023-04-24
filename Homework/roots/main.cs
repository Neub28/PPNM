using System;
using static System.Math;
using System.IO;
using static System.Console;

class main{
	public static Func<vector, vector> f;

	static void Main() {
	partA();

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

}


using System;
using static System.Math;

public static class cubature {
	private static double[] wi = { 2.0/6, 1.0/6, 1.0/6, 2.0/6 };
	private static double[] vi = { 1.0/4, 1.0/4, 1.0/4, 1.0/4  };
	private static double[] xi = { 1.0/6, 2.0/6, 4.0/6, 5.0/6  };

	/* Main method for two-dimensional integral. */
	public static (double, double) integ2D(Func<double, double, double> f, double a, double b, 
	Func<double, double> d, Func<double, double> u, double acc, double eps) 
	{
		double h = b-a;

		/* Steps 2 and 3 */
		
		double x2 = a+h*xi[1];
		double x3 = a+h*xi[2];
		
		/* Calculate dy limits for x2 and x3 */
		
		double d2 = d(x2);
		double u2 = u(x2);

		double d3 = d(x3);
		double u3 = u(x3);

		double h2 = u2-d2;
		double h3 = u3-d3;

		/* New x's for dy limits */
		
		double x2_1 = d2+h2*xi[1];
		double x2_2 = d2*h2*xi[2];
		
		double x3_1 = d3+h3*xi[1];
		double x3_2 = d3+h3*xi[2];

		/* Calculate 1d integrals for fixed x2 and x3 x-values. */
		/* Uses recursion until integral is determined with given accuracy */
		
		double f2 = integ1D(f, x2, d2, u2, acc, eps, f(x2, x2_1), f(x2, x2_2));
		double f3 = integ1D(f, x3, d3, u3, acc, eps, f(x3, x3_1), f(x3, x3_2));
		
		/* Do recursion */
		
		(double i, double e) = recursion2D(f, a, b, d, u, acc, eps, f2, f3);
		return (i,e);	

	}  
	
	/* One dimensional integrator used by integ2D for calculation of integral with fixed x-value. Note a ~ d(x_n) & b ~ u(x_n) */
	/* (Structure is alike homework)  */
	private static double integ1D(Func<double, double, double> f, double x, double a, double b, double acc, double eps, double f2, double f3) 
	{
		double h = b-a;
		/* Steps 1 and 4 */
		double x1 = a+h*xi[0];
		double x4 = a+h*xi[3];
		/* Evalutions of function */
		double f1 = f(x, x1);
		double f4 = f(x, x4);

		/* Calculate Q and q */
		double Q = h*(wi[0]*f1 + wi[1]*f2 + wi[2]*f3 + wi[3]*f4);
		double q = h*(vi[0]*f1 + vi[1]*f2 + vi[2]*f3 + vi[3]*f4);

		if(Abs(Q-q) <= acc+eps*Abs(Q)) {
			/* Accept */
			return Q;
		}
		else {
			/* Adaptive */
			return integ1D(f, x, a, (a+b)/2, acc/Sqrt(2), eps, f1, f2) + integ1D(f, x, (a+b)/2, b, acc/Sqrt(2), eps, f3, f4);
		}


	}
	/* Two dimensional integrator using recursion to evalute integral. */
	private static (double, double) recursion2D(Func<double, double, double> f, double a, double b, 
	Func<double, double> d, Func<double, double> u, double acc, double eps, double f2, double f3)
	{
	
		double h = b-a;

		double x1 = a+h*xi[0];
		double d1 = d(x1);
		double u1 = u(x1);

		double x4 = a+h*xi[1];
		double d4 = d(x4);
		double u4 = u(x4);

		double h1 = u1-d1;
		double h4 = u4-d4;

		double x1_1 = d1+h1*xi[1];
		double x1_2 = d1+h1*xi[2];
		double x4_1 = d4+h4*xi[1];
		double x4_2 = d4+h4*xi[2];

		double f1 = integ1D(f, x1, d1, u1, acc, eps, f(x1, x1_1), f(x1, x1_2));
		double f4 = integ1D(f, x4, d4, u4, acc, eps, f(x4, x4_1), f(x4, x4_2));

		/* Calculate Q and q (trapezium- and rectangle-rule) */
		double Q = h*(wi[0]*f1+wi[1]*f2+wi[2]*f3+wi[3]*f4);
		double q = h*(vi[0]*f1+vi[1]*f2+vi[2]*f3+vi[3]*f4);

		if(Abs(Q-q) <= acc+eps*Abs(Q)) {
			/* Accept */
			return (Q, Abs(Q-q));
		}
		else {
			/* Adaptive */
			(double i1, double e1) = recursion2D(f, a, (b+a)*0.5, d, u, acc/Sqrt(2), eps, f1, f2);
			(double i2, double e2) = recursion2D(f, (b+a)*0.5, b, d, u, acc/Sqrt(2), eps, f3, f4);
			return (i1+i2, (e1+e2)*0.5);
		}
	
	}

} // End class

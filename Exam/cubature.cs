using System;
using static System.Math;

public static class cubature {
	private static double[] wi = { 2.0/6, 1.0/6, 1.0/6, 2.0/6 };
	private static double[] vi = { 1.0/4, 1.0/4, 1.0/4, 1.0/4  };
	private static double[] xi = { 1.0/6, 2.0/6, 4.0/6, 5.0/6  };

	/* Rescale (abbrv. for rsc) value, x, according to integration bounds, a & b.  */
	private static Func<double, double, double, double> rsc = (a,b,x) => { return a+(b-a)*x; };

	/* Main method for two-dimensional integral. */
	public static (double, double) integ2D(Func<double, double, double> f, double a, double b, 
	Func<double, double> d, Func<double, double> u, double acc, double eps, 
	double f2 = double.NaN, double f3 = double.NaN ) 
	{
		/* Check if it is first call */
		if( double.IsNaN(f2) && double.IsNaN(f3) ) {

			/* Steps 2 and 3 */
			double x2 = rsc(a, b, xi[1]);
			double x3 = rsc(a, b, xi[2]);
		
			/* Calculate dy limits for x2 and x3 */
			double d2 = d(x2);	double u2 = u(x2);
			double d3 = d(x3);	double u3 = u(x3);

			/* Step 2 and 3 for y's according to limits (d, u).*/
			double y2_1 = rsc(d2, u2, xi[1]);	double y2_2 = rsc(d2, u2, xi[2]);
		
			double y3_1 = rsc(d3, u3, xi[1]);	double y3_2 = rsc(d3, u3, xi[2]);

			/* Calculate 1d integrals for fixed x2 and x3 x-values. */
			/* Uses recursion until integral is determined with given accuracy */
			f2 = integ1D(f, x2, d2, u2, acc, eps, f(x2, y2_1), f(x2, y2_2));
			f3 = integ1D(f, x3, d3, u3, acc, eps, f(x3, y3_1), f(x3, y3_2));
		}
		/* Step 1 & 4 + possibly recursion... */	
		
		double x1 = rsc(a, b, xi[0]);	double d1 = d(x1);	double u1 = u(x1);
		double x4 = rsc(a, b, xi[3]);	double d4 = d(x4);	double u4 = u(x4);

		double y1_1 = rsc(d1, u1, xi[1]);	double y1_2 = rsc(d1, u1, xi[2]);
		double y4_1 = rsc(d4, u4, xi[1]);	double y4_2 = rsc(d4, u4, xi[2]);

		double f1 = integ1D(f, x1, d1, u1, acc, eps, f(x1, y1_1), f(x1, y1_2));
		double f4 = integ1D(f, x4, d4, u4, acc, eps, f(x4, y4_1), f(x4, y4_2));

		/* Calculate Q and q (trapezium- and rectangle-rule) */
		double Q = (b-a)*(wi[0]*f1+wi[1]*f2+wi[2]*f3+wi[3]*f4);
		double q = (b-a)*(vi[0]*f1+vi[1]*f2+vi[2]*f3+vi[3]*f4);

		if(Abs(Q-q) <= acc+eps*Abs(Q)) {
			/* Accept */
			return (Q, Abs(Q-q));
		}
		else {
			/* Adaptive */
			(double i1, double e1) = integ2D(f, a, (b+a)*0.5, d, u, acc/Sqrt(2), eps, f1, f2);
			(double i2, double e2) = integ2D(f, (b+a)*0.5, b, d, u, acc/Sqrt(2), eps, f3, f4);
			return (i1+i2, (e1+e2)*0.5);
		}	

	}  
	
	/* One dimensional integrator used by integ2D for calculation of integral with fixed x-value. Note a ~ d(x_n) & b ~ u(x_n) */
	/* (Structure is alike class in homework)  */
	private static double integ1D(Func<double, double, double> f, double x, double a, double b, 
	double acc, double eps, double f2, double f3) 
	{
		double h = b-a;
		/* Steps 1 and 4 */
		double x1 = rsc(a,b, xi[0]);
		double x4 = rsc(a,b, xi[3]);
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

} // End class

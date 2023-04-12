using System;
using static System.Math;

public static class integrator {
	
	public static double integrate(
	Func<double, double> f, double a, double b,
	double d = 0.001, double e=0.001,
	double f2 = double.NaN, double f3 = double.NaN ) 
	{
	double h = b - a; 
	/* Check if it is the first call */
	if(double.IsNaN(f2)) { f2 = f(a+2*h/6); f3 = f(a+4*h/6);  }
	double f1 = f(a+h/6);
	double f4 = f(a+5*h/6);
	/* Trapezium rule (higher order) */
	double Q = (2*f1+f2+f3+2*f4)/6*(b-a);
	/* Rectangle rule (lower order) */	
	double q = (f1+f2+f3+f4)/4*(b-a);
	
	double err = Abs(Q-q);
	double tol = d + e * Abs(Q);
	if( err <= tol ) return Q;
	else return (double) integrator(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2) + integrator(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
 	
		//double Q1 = integrate(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2);
		//double Q2 = integrate(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
	}

}

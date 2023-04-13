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
	else return integrate(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2) + integrate(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
	}
	
	public static double ccintegrate
	(
	Func<double, double> f, 
	double a, 
	double b, 
	double d = 0.001,
	double e = 0.001 
	) {
	Func<double, double> f_new = delegate(double theta) { 
		return f((a+b)/2+(b-a)/2*Cos(theta))*Sin(theta)*(b-a)/2; 
		};
	return integrate(f_new, 0, PI, d, e);

	}
	
	public static bool approx(double a, double b, double d=0.001, double e=0.001) {
	/* Check for absolute accuracy  */
	if(Abs(a-b) < d) return true;
	/* Check for relative accuracy */
	if(Abs(a-b)/Max(Abs(a), Abs(b)) < e) return true;
	/* No passed tests -> false */
	return false;

	}

	public static double erf(double z) {
	Func<double, double> f;
	if(z < 0.0) return -erf(-z);
	if(z >= 0.0 && z <= 1.0) {
		f = delegate(double x) { return Exp(-Pow(x,2)); };
		return 2.0/Sqrt(PI)*integrate(f, 0, z);
		}
	else {
		f = delegate(double x) { return Exp(-Pow(z+(1-x)/x, 2))/(x*x); };
		return 1.0-2.0/Sqrt(PI)*integrate(f, 0, 1);		

		}


	}
 
}

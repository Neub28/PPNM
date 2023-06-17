using System;
using static System.Math;

public static class integrator {
	
	// Method implementing adaptive quadrature integration
	private static (double, double) adaptive_quadrature( Func<double, double> f, double a, double b, double d = 0.001, double e=0.001, 
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
		if( err <= tol ) return (Q, err);
		else {
			
			var (y1, e1) = adaptive_quadrature(f, a, (a+b)/2, d/Sqrt(2), e, f1, f2);
			var (y2, e2) = adaptive_quadrature(f, (a+b)/2, b, d/Sqrt(2), e, f3, f4);
			return (y1+y2, (e1+e2)/2.0);

			}
	}
	
	public static (double, double) ccintegrate( Func<double, double> f, double a, double b, double d = 0.001, double e = 0.001 ) 
	{
		Func<double, double> f_new = delegate(double theta) { 
			return f((a+b)/2+(b-a)/2*Cos(theta))*Sin(theta)*(b-a)/2; 
			};
		return adaptive_quadrature(f_new, 0, PI, d, e);

	}
	
	// Method using the adaptive quadrature depending on circumstance
	public static (double, double) integrate( Func<double, double> f, double a, double b, double d=0.001, double e=0.001 )
	{
		Func<double, double> fnew;

		if(double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b)) {
			fnew = x => {
				return f(x/(1-x*x))*(1+x*x)/Pow(1-x*x,2);
			};
			return adaptive_quadrature(fnew, -1, 1, d, e);
		}
		if(!double.IsNegativeInfinity(a) && double.IsPositiveInfinity(b)) {
			fnew = x => {
				return f(a+x/(1-x))*1/Pow(1-x,2);
			};
			return adaptive_quadrature(fnew, 0, 1, d, e);
		}
		if(double.IsNegativeInfinity(a) && !double.IsPositiveInfinity(b)) {
			fnew = x => {
				return f(b+x/(1+x))*1/Pow(1+x,2);
			};
			return adaptive_quadrature(fnew, -1, 0, d, e);
		}
		else {
			return adaptive_quadrature(f, a, b, d, e);
		}

	}
	
	public static bool approx(double a, double b, double d=0.001, double e=0.001) 
	{
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
			return 2.0/Sqrt(PI)*integrate(f, 0, z).Item1;
			}
		else {
			f = delegate(double x) { return Exp(-Pow(z+(1-x)/x, 2))/(x*x); };
			return 1.0-2.0/Sqrt(PI)*integrate(f, 0, 1).Item1;		

			}
	}
 
}//End class

using System;
using static System.Math;
using static System.Console;

class main {

	public static void Main() {
		
		int count;
		double i;
		double e;
		Func<double, double, double> f;
		Func<double, double> d;
		Func<double, double> u;
		double a;
		double b;

		count = 0;
		f = (x,y) => { count++; return 42*y*y-12*x;  };
		d = x => { return Pow(x-2,2); };
		u = x => { return 6; };
		a = 0;
		b = 4;

		(i, e) = cubature.integ2D(f, a, b, d, u, 1e-5, 1e-5);

		WriteLine($"	Function		Bounds (a,b,d,u)		Estimate		Error			Exact		Counts");
		Write("	42y²-12x		0,4 & (x-2)²,6			");
		print(i,e, 11136, count);
		
		count = 0;
		f = (x,y) => { count++; return 2*y*x*x+9*y*y*y; };
		d = x => { return 2.0/3*x; };
		u = x => { return 2*Sqrt(x); };
		a = 0;
		b = 9;

		(i, e) = cubature.integ2D(f, a, b, d, u, 1e-5, 1e-5);
		Write("	2yx²+9y³		0,9 & 2x/3,2Sqrt(x)		");
		print(i,e, 4811.4, count);

		count = 0; 
		f = (x,y) => { count++; return x*(y-1); };
		d = x => x*x-3;
		u = x => 1-x*x;
		a = -Sqrt(2);
		b = Sqrt(2);
		(i,e) = cubature.integ2D(f, a, b, d, u, 1e-5, 1e-5);
		Write("	x(y-1)			-Sqrt(2),Sqrt(2) & x²-3,1-x²	");
		print(i,e, 0, count);

	}
	public static void print(double i, double e, double exact, double c) {
		Write($"{i}	{e}	{exact}		{c}");
		WriteLine("");
	}

} // End class

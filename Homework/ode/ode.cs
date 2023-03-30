using System;
using static System.Math;

public static class ode {

	public static (vector, vector) rkstep12(
		Func<double, vector, vector> f,
		double x,
		vector y,
		double h
		) 
	{
		vector k0 = f(x,y);
		vector k1 = f(x + h/2, y + k0 * (h/2));
		vector yh = y + k1*h;
		vector er = (k1 - k0) * h;

		return (yh, er);

	}
	
	public static (genlist<double>, genlist<vector>) driver ( 
		Func<double, vector, vector> f,
		double a,
		vector ya,
		double b,
		double h = 0.01,
		double acc = 0.01, 
		double eps = 0.01
		)
	{
		if(a > b) throw new ArgumentException("Driver: a > b");
		double x = a;
		vector y = ya.copy();

		var xlist = new genlist<double>();
		xlist.add(x);

		var ylist = new genlist<vector>();
		ylist.add(y);
		
		while(true) {
			if(x >= b) return (xlist, ylist);
			if(x + h > b) h = b - x;
			var (yh, erv) = rkstep12(f,x,y,h);
			double tol = Max(acc, yh.norm()*eps) * Sqrt(h/(b-a));
			double err = erv.norm();
			if(err <= tol) {
				x += h;
				y = yh;
				xlist.add(x);
				ylist.add(y);
			}
			h *= Pow(tol/err, 0.25)*0.95;
		}

	}
}

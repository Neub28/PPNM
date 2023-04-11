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
	
	public static vector driver ( 
		Func<double, vector, vector> f,
		double a,
		vector ya,
		double b,
		genlist<double> xlist = null,
		genlist<vector> ylist = null, 
		double h = 0.01,
		double acc = 0.01, 
		double eps = 0.01
		)
	{
		if(a > b) throw new ArgumentException("Driver: a > b");
		double x = a;
		vector y = ya.copy();

		if(xlist != null && ylist != null) {
			xlist.add(x);
			ylist.add(y);
		}
		
		while(true) {
			if(x >= b) return y;
			if(x + h > b) h = b - x;
			var (yh, erv) = rkstep12(f,x,y,h);
			
			vector tol = new vector(erv.size);
			for(int i = 0; i < y.size; i++) {
				tol[i] = (acc+eps*Abs(yh[i]))*Sqrt(h/(b-a));
			}
			bool ok = true;
			for(int i = 0; i < y.size; i++) {
				if(! (erv[i]<tol[i])) ok = false;
			}
			if(ok == true) { 
				x += h; 
				y = yh; 
				if(xlist != null && ylist != null) {
					xlist.add(x);
					ylist.add(y);
				}
			}
			double factor = tol[0]/Abs(erv[0]);
			for(int i = 1; i < y.size; i++) {
				factor = Min(factor, tol[i]/Abs(erv[i]));
			}
			h *= Min(Pow(factor,0.25)*0.95, 2);
		}

	}
}

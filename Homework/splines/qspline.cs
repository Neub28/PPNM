using System;
using static System.Math;

public class qspline {
	public double[] x, y, b, c, up_c, down_c;
	public int leng;

	public qspline(double[] xs, double[] ys) {
		/* Initialize arrays */
		leng = xs.Length;
		if(leng != ys.Length) throw new Exception("Arrays not same size");
		
		x = new double[leng];
		y = new double[leng];
		
		for(int i = 0; i < leng; i++) {
			x[i] = xs[i];
			y[i] = ys[i];
		}

		b = new double[leng-1];
		up_c = new double[leng-1];
		down_c = new double[leng-1];
		c = new double[leng-1];

		/* Set first elements */
		up_c[0] = 0;
		down_c[leng-2] = 0;
		b[0] = (y[1]-y[0])/(x[1]-x[0]);

		/* Upward recursion and calculate p[i] */
		for(int i = 1; i < leng-1; i++) {
			double dx = x[i+1] - x[i];
			double dy = y[i+1] - y[i];

			b[i] = dy/dx; // at this point it is p[i]
			
			up_c[i] = 1/dx*(b[i] - b[i-1] - up_c[i-1]*(x[i]-x[i-1]));
		}
		/* Downward recursion */ 
		for(int i = leng-3; i >= 0; i--) {
			double dx = x[i+1] - x[i];

			down_c[i] =  1.0/dx * (b[i+1] - b[i] - down_c[i+1] * (x[i+2]-x[i+1]));
		}
		/* Averageing the downward- and upward recursion. */
		for(int i = 0; i < leng-1; i++) {
			down_c[i] += up_c[i];
			down_c[i] /= 2.0;
			c[i] = down_c[i];
		}
		/* Remake p-array to b-array */
		for(int i = 0; i < leng-1; i++) {
			b[i] -= c[i] * (x[i+1]-x[i]);
		}

	}
	
	public static int binsearch(double[] x, double z) {
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("binsearch: bad z");
		int i=0, j=x.Length-1;
		while(j-i>1){
			int mid=(i+j)/2;
			if(z>x[mid]) i=mid; else j=mid;
		}
		return i;

	}

	public double evalute(double z) {
		int i = binsearch(x, z);
		
		return y[i] + b[i]*(z-x[i]) + c[i]*Pow(z-x[i],2);

	}


}



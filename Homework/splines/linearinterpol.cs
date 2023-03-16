using System;

public class linearinterpol {
	/* Binary search  */
	public static int binsearch(double[] x, double z) {
		if(!(x[0]<=z && z<=x[x.Length-1])) throw new Exception("Exception: binsearch");
		int i = 0, j = x.Length-1;
		while(j - i > 1) {
			int mid = (i+j)/2;
			if(z>x[mid]) i = mid; 
			else j = mid;
		}
		return i;
	}
	public static double linterp(double[] x, double[] y, double z) {
		int i = binsearch(x, z);
		double dx = x[i+1] - x[i];
		if(!(dx>0)) throw new Exception("Exception: dx");
		double dy = y[i+1] - y[i];

		return y[i]+dy/dx*(z-x[i]);
	}
	public static double linterpInteg(double[] x, double[] y, double z) {
		int iz = binsearch(x, z);
		double sum = 0, dx, dy, pi;
		for(int i = 0; i <= iz; i++) {
			dx = x[i+1] - x[i];
			dy = y[i+1] - y[i];
			pi = dy/dx;
			
			if(i == iz) { dx = z - x[iz]; }

			sum += y[i]*dx+pi*(0.5*dx*dx);
		}
		return sum;
		
	}

}

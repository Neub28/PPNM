using System; 
using static System.Math;
using static System.Console;

public static class simplex {

	public static double size(vector[] vec) {
		double dist = 0;
		for(int i = 1; i < vec.Length; i++) {
			dist = Math.Max(dist, (vec[i]-vec[0]).norm());
		}
		return dist;
	}

	public static void downhill
	(Func<vector, double> f, vector init, double sizegoal, double step=1.0/64, double countlim = 100000) {
		int counts = 0;
		int dim = init.size;
		int npoints = dim + 1;
		vector[] vecs = new vector[npoints];
	
		/* Last element is initial vector. All other elements are steps in all possible directions. */
		vecs[npoints] = init.copy();
	
		for(int i = 0; i < dim; i++) {
			init[i] += step;
			vecs[i] = init.copy();
			init[i] -= step;

		}
		foreach(vector v in vecs) {
			Error.WriteLine($"{v[0]}  {v[1]}  {v[2]}");
		}
	
		int hi = 0;
		int lo = 0;
		vector phi = vecs[hi];
		vector plo = vecs[lo];
		vector pce = vecs[0];



		/* REPEAT  */

	}

}

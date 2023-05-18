using System;
using static System.Math;
using static System.Console;

public static class minimisation {
	
	/* Used to calculate the gradient of a function at vector x */
	public static vector gradient(	
					Func<vector, double> f, 
					vector x
					)
	{
	double dx = Pow(2, -26);
	vector grad = new vector(x.size);
	for(int i = 0; i < x.size; i++) {
		vector xdx = x.copy();
		xdx[i] += dx;
		grad[i] = (f(xdx) - f(x))/dx;
	}
	return grad;

	}
	
	/* Quasi-Newton method with numerical gradient  */
	public static (vector, int) qnewton(
						Func<vector, double> f, 
						vector start,
						double acc	
						)
	{
	int counter = 0;
	int dim = start.size;
	vector x = start.copy();
	matrix B = new matrix(dim, dim);
	B.setid();
	
	while(gradient(f, x).norm() > acc) {
		counter ++;
		vector grad = gradient(f, x);
		vector dx = -B*grad;
		double lambda = 1.0;
		while(true) {
			if(f(x+lambda*dx) < f(x)) {
				/* Accept step and update B */
				x += lambda*dx;
				vector graddx = gradient(f, x);
				vector y = graddx-grad;
				vector u = dx-B*graddx;
				double uy = u%y;
				if(Abs(uy) > Pow(2, -26)) {
					matrix dB = matrix.outer(u, u)/uy;
					B += dB;
				}
				break;	
			}
			lambda /= 2.0;
			double lim = 1.0/1024.0;
			if(lambda < lim) {
				x += lambda*dx;
				B.setid();
				break;
			}


		}

	}
	return (x, counter);	

	}
	



	public static (vector, int) simplex
	(Func<vector, double> f, vector init, double sizegoal, double step, double countlim = 100000) {
	int dim = init.size;
	int npoints = dim + 1;
	vector[] vecs = new vector[npoints];
	double[] fs = new double[npoints];
	
	/* Last element is initial vector. All other elements are steps in all possible directions. */
	vecs[dim] = init.copy();
	
	for(int i = 0; i < dim; i++) {
		init[i] += step;
		vecs[i] = init.copy();
		fs[i] = f(vecs[i]);
		init[i] -= step;

	}
	
	int hi = 0;
	int lo = 0; 
	int counts = 0;

	/* REPEAT  */
	do {
		counts ++;
		hi = 0; lo = 0;
		double fhi = fs[hi];
		double flo = fs[lo];

		if(counts > countlim) {
			Error.WriteLine("Number of operations exceeded.");
		}
		/* Find highest-, lowest- and centroid-point(s) */
		for(int i = 1; i < npoints; i++) {
			if(fs[i] > fhi ) fhi = fs[i]; hi = i;
			if(fs[i] < flo ) flo = fs[i]; lo = i;
		}
		vector pce = new vector(dim);
		for(int i = 0; i < npoints; i++) {
			if(i != hi) pce += vecs[i];
		}
		pce /= dim;

		
		foreach(vector v in vecs) {
			Error.WriteLine($"{v[0]}  {v[1]}  {v[2]}");
		}

		/* Try reflection */	
		vector pre = pce+pce-vecs[hi];

		if(f(pre) < flo) {
			/* Try expansion */
			vector pex = pce+2*(pce-vecs[hi]);
			if(f(pex) < f(pre)) {
				/* Accept expansion */
				vecs[hi] = pex;
			}
			else {
				/* Accept reflection */
				vecs[hi] = pre;
			}
		}
		else {
			if(f(pre) < fhi) {
				/* Accept reflection */
				vecs[hi] = pre;
			}
			else {
				/* Try contraction */
				vector pco = pce+0.5*(vecs[hi]-pce);
				if(f(pco) < fhi) {
					/* Accept contraction */
					vecs[hi] = pco;
				}
				else {
					/* Do reduction */
					for(int i = 0; i < npoints; i++) {
						if(i != lo) {
							vecs[i] = 0.5*(vecs[i]+vecs[lo]);
						}
					}
					}
				}
			}
	} while(simplex_size(vecs) > sizegoal && counts < countlim);
	
	Error.WriteLine($"size {simplex_size(vecs)} \t sizegoal {sizegoal}");

	return (vecs[lo], counts);
	}
	

	public static double simplex_size(vector[] vec) {
	double max_norm = 0;
	for(int i = 1; i < vec.Length; i++) {
		max_norm = Math.Max(max_norm, (vec[i]-vec[0]).norm());
		}
	return max_norm;
	}


}

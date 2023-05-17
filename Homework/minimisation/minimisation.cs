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
	(Func<vector, double> f, vector init, double sizegoal, double step=1.0/64, double countlim = 100000) {
	int dim = init.size;
	int npoints = dim + 1;
	vector[] vecs = new vector[npoints];
	
	/* Last element is initial vector. All other elements are steps in all possible directions. */
	vecs[dim] = init.copy();
	
	for(int i = 0; i < dim; i++) {
		init[i] += step;
		vecs[i] = init.copy();
		init[i] -= step;

	}
	
	int hi = 0;
	int lo = 0; 
	int counts = 0;
	vector phi = vecs[hi];
	vector plo = vecs[lo];

	/* REPEAT  */
	do {
		counts ++;
		hi = 0; lo = 0;
		phi = vecs[hi];
		plo = vecs[lo];

		if(counts > countlim) {
			Error.WriteLine("Number of operations exceeded.");
		}
		/* Find highest-, lowest- and centroid-point(s) */
		for(int i = 1; i < npoints; i++) {
			if(f(vecs[i]) > f(phi) ) phi = vecs[i]; hi = i;
			if(f(vecs[i]) < f(plo) ) plo = vecs[i]; lo = i;
		}
		vector pce = new vector(dim);
		for(int i = 1; i < npoints; i++) {
			if(i != hi) pce += vecs[i];
		}
		pce /= dim;

		
		foreach(vector v in vecs) {
			Error.WriteLine($"{v[0]}  {v[1]}  {v[2]}");
		}

		/* Try reflection */	
		vector pre = pce+pce-phi;
		if(f(pre) < f(plo)) {
			/* Try expansion */
			vector pex = pce+2*(pce-phi);
			if(f(pex) < f(pre)) {
				/* Accept expansion */
				phi = pex;
			}
			else {
				/* Accept reflection */
				phi = pre;
			}
		}
		else {
			if(f(pre) < f(phi)) {
				/* Accept reflection */
				phi = pre;
			}
			else {
				/* Try contraction */
				vector pco = pce+0.5*(phi-pce);
				if(f(pco) < f(phi)) {
					/* Accept contraction */
					phi = pco;
				}
				else {
					/* Do reduction */
					for(int i = 0; i < npoints; i++) {
						if(i != lo) {
							vecs[i] = 0.5*(vecs[i]-plo);
						}
					}
					}
				}
			}
	} while(simplex_size(vecs) > sizegoal && counts < countlim);
	
	Error.WriteLine($"size {simplex_size(vecs)} \t sizegoal {sizegoal}");

	return (plo, counts);
	}
	

	public static double simplex_size(vector[] vec) {
	double max_norm = 0;
	for(int i = 1; i < vec.Length; i++) {
		max_norm = Math.Max(max_norm, (vec[i]-vec[0]).norm());
		}
	return max_norm;
	}


}

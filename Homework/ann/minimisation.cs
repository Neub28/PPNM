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
	int second_count = 0;
	int dim = start.size;
	vector x = start.copy();
	matrix B = new matrix(dim, dim);
	B.setid();
	
	//Error.WriteLine("Quasi-Newton routine started..... ");
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
			double lim = Pow(2,-20);
			if(lambda < lim) {
				x += lambda*dx;
				B.setid();
				break;
			}
			second_count ++;


		}

	}
	//Error.WriteLine("Quasi-Newton done.");
	return (x, counter);	

	}
	



	public static (vector, int) simplex
	(Func<vector, double> f, vector init, double sizegoal, double step, double countlim = 100000) {
	
	int dim = init.size;
	int npoints = init.size+1;
	vector[] ps = new vector[npoints]; 
	double[] fps = new double[npoints];
	
	ps[dim] = init.copy();
	fps[dim] = f(ps[dim]);
	
	/* Use step to create the other vectors of the simplex */
	for(int i = 0; i < dim; i++) {
		init[i] += step;
		ps[i] = init.copy();
		fps[i] = f(ps[i]);
		init[i] -= step;
	}
	/* Variables storing index of highest and lowest points. */
	int hi = 0;
	int lo = 0;
	/* Variables for operations. */
	vector pre;
	vector pex;
	vector pco;

	int count = 0;
	
	Error.WriteLine("Simplex is now rolling 'downhill'.... ");
	do {
		hi = 0;
		lo = 0;
		count ++;
		if(count != 0 && count % 10000 == 0) {
			Error.WriteLine($"{count} tries...");
		}

		/* Find highest and lowest points */
		for(int i = 1; i < npoints; i++) {
			if(f(ps[i]) > f(ps[hi])) { hi = i; }
			if(f(ps[i]) < f(ps[lo])) { lo = i; }
			}
		/* Calculate centroid point  */
		vector pce = new vector(dim);
		for(int i = 0; i < npoints; i++) {
			if(i!=hi) { pce += ps[i];  }
		}
		pce /= dim;
		
		/* Reflection  */
		pre = 2*pce - ps[hi];
		if( f(pre) < f(ps[lo]) ) {
			/* Expansion */
			pex = 3*pce - ps[hi];
			if( f(pex) < f(pre)  ) {
				/* Accept expansion */
				ps[hi] = pex;
				}
			else {
				/* Accept reflection */
				ps[hi] = pre;
				}
			}
		else {
			if( f(pre) < f(ps[hi])  ) {
				/* Accept reflection */
				ps[hi] = pre;
				}
			else {
				/* Contraction  */
				pco = (pce+ps[hi])/2;
				if( f(pco) < f(ps[hi])  ) {
					/* Accept contraction  */
					ps[hi] = pco;
					}
				else {
					/* Reduction */
					for(int i = 0; i < npoints; i++) {
						if(i!=lo) {
							ps[i] = (ps[i]+ps[lo])/2;
							fps[i] = f(ps[i]);
							}
						}

					}

				}
			}
	//Error.WriteLine($"size: {simplex_size(ps)} and sizegoal: {sizegoal}");
	} while(simplex_size(ps) > sizegoal && count < countlim);
	if(count == countlim) Error.WriteLine("Numbers of counts exceeded.");
	Error.WriteLine("Downhill-simplex done.");
	return (ps[lo], count);
	}
	

	public static double simplex_size(vector[] vec) {
	double max_norm = 0;
	for(int i = 1; i < vec.Length; i++) {
		max_norm = Math.Max(max_norm, (vec[i]-vec[0]).norm());
		}
	return max_norm;
	}


}

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
	
	while(gradient(f, x).norm() > acc) {
		counter ++;
		vector grad = gradient(f, x);
		vector dx = -B*grad;
		double lambda = 1.0;
		while(true) {
			if(second_count > 25000) {
				Error.WriteLine("No convergence. Bad start");
				throw new OperationCanceledException("Too many tries....");
			}
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
			second_count++;


		}

	}
	return (x, counter);	

	}


}

using System;
using static System.Math;

public static class roots {

	public static vector newton(	Func<vector, vector> f,
					vector x, 
					double eps = 1e-2
					)
	{
	int dim = x.size;
	vector xnew;
	
	do {
		/* Step based upon square root of machine epsion. */
		double dx = x.norm()*Pow(2, -26);
		if(dx == 0) { dx = Pow(2, -26); }
		/* Calculate Jacobian matrix numerically */
		matrix J = new matrix(dim, dim);
		for(int k = 0; k < dim; k++) {
				/* Make vector with step  */
				xnew = x.copy();
				xnew[k] += dx;
				J[k] = (f(xnew)-f(x))/dx;
		}
		/* Solve matrix equation */
		QRGS qr = new QRGS(J);
		xnew = qr.solve(-f(x));
		
		double lambda = 1.0;
		
		/* Backtracking with lambda  */
		while(f(x+lambda*xnew).norm() > (1-lambda/2.0)*f(x).norm() && lambda > 1.0/32) {
		lambda /= 2;
		}

		x += lambda*xnew;
			
	} while(f(x).norm() > eps);
	
	return x;

	} 
}	



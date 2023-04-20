using System;
using static System.Math;

public static class mcint {
	// Variable of primes for Halton
	public static int[] primes1 = {2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61};
	public static int[] primes2 = {67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131};
	
	/* Implementation of Van der Corput algorithm */
	public static double corput(int n, int b) {
	double q = 0;
	double bk = (double) 1.0/b;
	while(n > 0) { 
		q += ( n % b ) * bk; 
		n /= b;
		bk /= b;
	}
	return q;
	}
	/* Halton-algorithm: Takes int-array of primes as sequence */
	public static void halton(int n, int d, vector v, int[] prime, vector a, vector b) {
	/* Fill values in vector v with Corput */
	for(int i = 0; i < d; i++) {
		v[i] = corput(n, prime[i])*(b[i]-a[i]);
	}
	}
	
	/* Plain Monte Carlo integrator ranging from vector a to vector b */
	public static (double, double) plainmc(	Func<vector, double> f, 
						vector a,
						vector b, 
						int N) 
	{
	int dim = a.size; 
	double V = 1; 
	for(int i = 0; i < dim; i++) V *= Abs(b[i]-a[i]);
	double sum = 0; 
	double sum2 = 0;
	var x = new vector(dim);
	var rnd = new Random();
	for(int i = 0; i < N; i++) {
		for(int k = 0; k < dim; k++) { 
			x[k] = a[k] + rnd.NextDouble()*(b[k]-a[k]);
		}
		double fx = f(x); 
		sum += fx; 
		sum2 += fx*fx;
	}
	double mean = sum/N; 
	double sigma = Sqrt(sum2/N-mean*mean);

	var result = (mean*V, sigma*V/Sqrt(N));
	
	return result;

	}
	public static (double, double) qrand(	Func<vector, double> f,
						vector a,
						vector b,
						int N )
	{
	int dim = a.size;
	double V = 1;
	for(int i = 0; i < dim; i++) V *= b[i]-a[i];
	double sum = 0;
	double sum2 = 0;
	var v = new vector(dim);
	for(int i = 0; i < N; i++) {
		/* First sequence for integral */
		halton(i, dim, v, primes1, a, b);
		double fx = f(v);
		sum += fx;
		/* Second sequence for error estimation */
		halton(i, dim, v, primes2, a, b);
		sum2 += f(v);
	}
	double intg = sum/N*V;
	double err = Abs(sum-sum2)/N*V;
	return (intg, err);

	}

}

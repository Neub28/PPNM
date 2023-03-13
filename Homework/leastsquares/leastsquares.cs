using System;
using static System.Math;

public class leastsquares {
	public static vector lsfit(Func<double, double>[] fs, vector x, vector y, vector dy) {
		/* Initialize the matrix A and vector b (eq. 14)  */
		matrix A = new matrix(x.size, fs.Length);
		vector b = new vector(x.size);
		/* Loop over all rows  */
		for(int i = 0; i < x.size; i++) {
			/* Set values of b rows  */
			b[i] = y[i]/dy[i];
			/* Loop over all coloums */
			for(int k = 0; k < fs.Length; k++) {
				/* Set values of A */
				A[i,k] = fs[k](x[i])/dy[i];
			}
		}
		/* Use QR-decomposition on A  */
		QRGS QR_A = new QRGS(A);
		vector c = QR_A.solve(b);
		return c;
	}

}

using static System.Console;

class main{
	static void Main() {
		// Random variable
		var rnd = new System.Random(7);

		// TESTS FOR DECOMP

		// Rows and cols in random generated tall matrix
		int s1 = 6;
		int s2 = 5;

		matrix A = new matrix(s1, s2);

		// Initialize random values in matrix
		for(int row = 0; row < s1; row++) {
			for(int col = 0; col < s2; col++) {
				A[row, col] = rnd.NextDouble();
			}
		}
		
		// Use QRGS class
		QRGS A_dc = new QRGS(A);
		A_dc.Q.print("Q");
		A_dc.R.print("R");

		// Is Q orthonormal?
		matrix QTQ = A_dc.Q.transpose()*A_dc.Q;
		//QTQ.print("Q^T * Q; should be unity");
		WriteLine($"Is QTQ = 1? {QTQ.approx(matrix.id(s2))}");

		// IS Q*R = A?
		matrix QR = A_dc.Q * A_dc.R;
		//QR.print("Q*R; should be A");
		WriteLine($"Is QR = A? {QR.approx(A)}");
	
		// DETERMINANT 
		WriteLine($"det(A) = det(R) = {A_dc.det()}");


		// TESTS FOR SOLVE
		int size = 5;
		matrix B = new matrix(size, size);
		vector b = new vector(size);
		for(int row = 0; row < size; row ++) {
			b[row] = rnd.NextDouble();
			for(int col = 0; col < size; col ++) {
				B[row, col] = rnd.NextDouble();
			}
		}
		// Factorize B into QR
		QRGS B_dc = new QRGS(B);

		// Use solve
		B_dc.solve(b);

		// Check that QRx = b
		vector QRx = B_dc.Q * B_dc.R *  B_dc.solve(b);
		WriteLine($"Is QRx = b? {QRx.approx(b)}");
		
		// Check that R^-1 * Q^T * B is 1 by using inverse
		matrix B_inv_timesB = B_dc.inverse() * B;
		WriteLine($"Is R^-1*Q^T*B = 1? {B_inv_timesB.approx(matrix.id(size))}");
	}	
}

using static System.Console;

public class main {

	static void Main() {
		/* Pseudo random number */
		var rnd = new System.Random(7); /* Seed 7 */
		
		WriteLine("Task A: Jacobi diagonlization with cyclic sweeps");
		WriteLine("------------------------------------------------");

		/* Intialize a random symmetric matrix of moderate size  */
		int size = 5;
		matrix A = new matrix(size, size);
		for(int row = 0; row < size; row ++) {
			for(int col = row; col < size; col ++) {
				double value = rnd.NextDouble();
				A[row, col] = value;
				A[col, row] = value;
			}
			
		}
		matrix V = matrix.id(size);
		matrix A_before = A.copy();
		
		/* Do Jacobi diagonalization of matrix A */
		jacobi_diag.cyclic(A, V);
		
		/* TESTS */
		matrix D_test = V.transpose()*A_before*V;
		WriteLine($"Is V^T A V = D ? {D_test.approx(A)}");
		
		matrix A_test = V*A*V.transpose();
		WriteLine($"Is V D V^T = A ? {A_test.approx(A_before)}");

		matrix unity_1 = V.transpose()*V;
		WriteLine($"Is V^T V = 1 ? {unity_1.approx(matrix.id(size))}");

		matrix unity_2 = V*V.transpose();
		WriteLine($"Is V V^T = 1 ? {unity_2.approx(matrix.id(size))}");
		
		
		WriteLine("Hydrogen atom, s-wave radial Schrödinger equation of a grid");
		WriteLine("-----------------------------------------------------------");
		/* Initialize the Hamiltonian matrix */
		double rmax = 10, dr = 0,3;
		int npoints = (int) (rmax/dr)-1;
		vector r = new vector(npoints);
		for(int i = 0; i < npoints; i++) r[i] = dr*(i+1);
		matrix H = new matrix(npoints, npoints);
		for(int i = 0; i < npoints-1; i++) {
			matrix.set(H, i, i-2, -2);
			matrix.set(H, i, i+1, 1);
			matrix.set(H, i+1, i, 1);
		}
		matrix.set(H, npoints-1, npoints-1, -2);
		H *= -0.5/dr/dr;
		for(int i = 0; i < npoints; i++) H[i,i] += 1/r[i];

		/* Diagonalize the matrix with Jacobi algorithm */
		matrix V = matrix.id(npoints);
		jacobi_diag.cycle(H, V);

	}

}

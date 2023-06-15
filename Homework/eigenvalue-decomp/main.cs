using static System.Console;
using static System.Math;
using System;
using System.Diagnostics;

public class main {
	public static Random rnd = new System.Random(7);

	static void Main(string[] args) {

		double dr = 0; double rmax = 0;
		
		foreach(string arg in args) {
			string[] input = arg.Split(":");
			if(input[0] == "-partA") partA();
			if(input[0] == "-partB") partB();
			if(input[0] == "-dr") dr = double.Parse(input[1])*0.1;
			if(input[0] == "-rmax") rmax = double.Parse(input[1]);
			if(input[0] == "-wavefunc") {
			int n = Int32.Parse(input[1])-1;
			rmax = 10; dr = 0.2;
			var res = drrmax(dr, rmax);
			matrix V = res.Item3;
			for(int i = 0; i < V.size1; i++) {
				WriteLine($"{(i+1)*dr}	{V[i,n]*1/Sqrt(dr)}");
				}
			dr = 0; rmax = 0;
			}
			if(input[0] == "-partC") partC();
			if(input[0] == "-partCtest") partCtest();
		}

		if(dr != 0 && rmax != 0) {
			double lowestE = drrmax(dr, rmax).Item1;
			WriteLine($"{dr}	{rmax}	{lowestE}");
		}

	}

	static void partA() {
		WriteLine("Task A: Jacobi diagonlization with cyclic sweeps");
		WriteLine("------------------------------------------------");

		/* Intialize a random symmetric matrix of moderate size  */
		int size = 5;
		matrix A = new matrix(size, size);
		for(int row = 0; row < size; row ++) {
			for(int col = row; col < size; col ++) {
				double val = rnd.NextDouble();
				A[row, col] = val;
				A[col, row] = val;
			}
			
		}
		matrix V = matrix.id(size);
		matrix A2 = A.copy();
		matrix V2 = matrix.id(size);
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

		WriteLine("I am now testing the reliability of the optimized routine....");
		Error.WriteLine("B");
		jacobi_diag.optimizedCyclic(A2,V2);
		Error.WriteLine("A");
		D_test = V2.transpose()*A_before*V2;
		WriteLine($"Is V^T A V = D ? {D_test.approx(A2)}");

		A_test = V2*A2*V2.transpose();
		WriteLine($"Is V D V^T = A ? {A_test.approx(A_before)}");

		unity_1 = V2.transpose()*V2;
		WriteLine($"Is V^T V = 1 ? {unity_1.approx(matrix.id(size))}");

		unity_2 = V2*V2.transpose();
		WriteLine($"Is V V^T = 1 ? {unity_2.approx(matrix.id(size))}");

	}
	
	static void partB() {
		WriteLine("Hydrogen atom, s-wave radial Schrödinger equation of a grid");
		WriteLine("-----------------------------------------------------------");
		WriteLine("A plot of the lowest eigenvalues for varying rmax and Δr is in convergence.svg");
		WriteLine("From this plot we see that for around rmax = 10 and dr = 0.2 the \neigenvalue convergences to the exact solution.");
		WriteLine("In swavefunction.svg I have plotted the wavefunctions for lowest energies \nand different n's and compared them to the analytical results.");

	}

	static void partC() {
		WriteLine("Part C: Scaling and optimization");
		WriteLine("-----------------------------------------------------------");
		WriteLine("First task was to do convergence calculations in parallel.");
		WriteLine("In my Makefile I use a forloop over different values, therefore \nsimply using parallel batch processing '&' at end of each forloop solves this.");

	}
	
	static void partCtest() {
		int[] nlist = {10, 20, 50, 70, 100, 120, 150, 170, 200, 220, 250};
		long times1;
		long times2;
		foreach(int n in nlist) {
			(times1, times2) = diagtimes(n);
			WriteLine($"{n}	{times1}	{times2}");
		}

	}

	/* Method finds eigenvalue for certain choice of dr and rmax. */
	static (double, matrix, matrix) drrmax(double dr, double rmax) {
		
		int npoints = (int)(rmax/dr)-1;
		vector r = new vector(npoints);
		for(int i=0;i<npoints;i++)r[i]=dr*(i+1);
		matrix H = new matrix(npoints,npoints);
		for(int i=0;i<npoints-1;i++){
 			H[i,i]  =-2;
   			H[i,i+1]= 1;
   			H[i+1,i]= 1;
  		}
		H[npoints-1,npoints-1]=-2;
		matrix.scale(H,-0.5/dr/dr);
		for(int i=0;i<npoints;i++)H[i,i]+=-1/r[i];
		// Diagonalize the matrix with Jacobi algorithm
		matrix V = matrix.id(npoints);
		jacobi_diag.cyclic(H, V);
		// All eigenvalues are diagonal elements in H now.
		// Find lowest value

		double lowestE = H[0,0];
		for(int i = 1; i < npoints; i++) {
			if(H[i,i] < lowestE) lowestE = H[i,i];
		}
		return (lowestE, H, V);
	}

	static (long, long) diagtimes(int n) {
		matrix J = new matrix(n,n);
		matrix V = matrix.id(n);
		for(int row = 0; row < n; row ++) {
			for(int col = row; col < n; col ++) {
				double val = rnd.NextDouble();
				J[row, col] = val;
				J[col, row] = val;
			}
			
		}
		matrix J2 = J.copy();
		matrix V2 = V.copy();

		Stopwatch watch1 = new Stopwatch();
		Stopwatch watch2 = new Stopwatch();
		watch1.Start();
		jacobi_diag.cyclic(J,V);
		watch1.Stop();
		
		watch2.Start();
		jacobi_diag.optimizedCyclic(J2,V2);
		watch2.Stop();
		
		return (watch1.ElapsedMilliseconds, watch2.ElapsedMilliseconds);

	}
}

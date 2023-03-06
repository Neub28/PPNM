
public class convergence {

	public static void Main() {
	
	}
	public static void calculate_eps(rmax, dr) {
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
	public static void fixed_rmax() {
		var outstream = new StreamWriter("fixed_rmax.txt");
		double rmax = 10;
		for(double dr = 0.0; dr= 1.0; dr += 1/32) {
			outstream.WriteLine($"{rmax}	{
		}	

	}
	public static void fixed_dr() {
		var outstream = new StreamWriter("fixed_dr.txt");
	}

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

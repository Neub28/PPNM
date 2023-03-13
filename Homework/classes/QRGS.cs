
public class QRGS{
	// Field variables
	public matrix Q, R;
	
	// Constructor: Uses GS-orthogonlization to QR-decomp matrix A.
	public QRGS(matrix A) {
		Q = A.copy();
		R = new matrix(A.size2, A.size2);
		for(int i = 0; i < A.size2; i++) {
			R[i, i] = Q[i].norm();
			Q[i] /= R[i, i];
			for(int j = i+1; j < A.size2; j++) {
				R[i, j] = Q[i].dot(Q[j]);
				Q[j] -= Q[i] * R[i, j];
			}
		}	

	}
	// Solve QRx = b by back substitution.
	// return:	 (vector) x
	public vector solve(vector b) { 
		vector QTb = Q.transpose() * b;
		// In-place back-sub: Solution x is stored in vector QTb. 
		for(int i=QTb.size - 1; i >= 0; i--) {
			double sum = 0;
			for(int k = i+1; k<QTb.size; k++) sum += R[i, k] * QTb[k];
			QTb[i] = (QTb[i]-sum)/R[i,i];	
		}	
		return QTb;
	}
	// Determine the determinant of the matrix A, by using det(R) = det(A).
	// return:	(double) 
	public double det() { 
		double val = 1;
		for(int diag = 0; diag < R.size1; diag++) {
			val *= R[diag, diag];
		}
		return val;
	}
	// Calculates the inverse of A = QR by using back-subs. (solve). 
	// return:	(matrix) inverse
	public  matrix inverse() {
       		matrix inverse = new matrix(R.size1, R.size1);
		for(int eq = 0; eq < R.size1; eq++) {
			vector ei = new vector(R.size1); 
			ei[eq] = 1;
			inverse[eq] = solve(ei);	
		}
		return inverse;
	}
	
}

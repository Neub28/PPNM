using System;
using static System.Console;

public class QRtimes {
	public static int n;
	public static Random rnd = new Random(7);
	public static void Main(string[] args) {
		foreach(var arg in args){
			var words = arg.Split(':');
			if(words[0] == "-n") n = (int) double.Parse(words[1]);
			else throw new ArgumentException("No input given");
		}
		matrix A = new matrix(n, n);
		for(int col = 0; col < n; col++) {
			for(int row = 0; row < n; row++) {
				A[col, row] = rnd.NextDouble();
			}
		}
		QRGS A_QR = new QRGS(A);
	}
}

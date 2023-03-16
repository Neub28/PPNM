using System;
using System.IO;
using static System.Math;

class main {
	static void Main() {
		
		var pointstream = new StreamWriter("pointvalues.txt");
		
		/* Generate values of Sin(x) */
		int n = 5;
		double[] x = new double[n];
		double[] y = new double[n];

		for(int i = 0; i < n; i++) {
			x[i] = 2*PI*i/(n-1);
			y[i] = Sin(x[i]);
			pointstream.WriteLine($"{x[i]}	{y[i]}");	
		}
		pointstream.Close();
		
		var tabstream = new StreamWriter("tabvalues.txt");
		var intpolstream = new StreamWriter("intpolvalues.txt");
		var integralstream = new StreamWriter("integralvalues.txt");

		int N = 300;
		double step = (x[n-1]-x[0])/(N-1);
		for(int i = 0; i < N; i++ ) {
			double z = x[0]+i*step;
			double s = linearinterpol.linterp(x, y, z);
			double integral = linearinterpol.linterpInteg(x, y, z);

			tabstream.WriteLine($"{z}	{Sin(z)}");
			intpolstream.WriteLine($"{z}	{s}");
			integralstream.WriteLine($"{z}	{integral}");
			
		}
	}
}

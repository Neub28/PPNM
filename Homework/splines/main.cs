using System;
using System.IO;
using static System.Math;
using static System.Console;

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
		
		tabstream.Close();
		intpolstream.Close();
		integralstream.Close();
		



		/* Quadratic spline */
		int length = 5;
		double[] x_b = new double[length];
		double[] y1 = new double[length];
		double[] y2 = new double[length];
		double[] y3 = new double[length];

		for(int i = 1; i <= 5; i++) {
			x_b[i-1] = i;
			y1[i-1] = 1;
			y2[i-1] = i;
			y3[i-1] = Pow(i,2);
		}
		
		var qtabstream = new StreamWriter("qtabvalues.txt");

		for(int i = 0; i < 5; i++) {
			qtabstream.WriteLine($"{x_b[i]}	{y1[i]}	{y2[i]}	{y3[i]}");
		}
		qtabstream.Close();

		qspline q1 = new qspline(x_b, y1);
		qspline q2 = new qspline(x_b, y2);
		qspline q3 = new qspline(x_b, y3);
		
		var qstream = new StreamWriter("qsplinevalues.txt");		
		
		double step2 = (x_b[4]-x_b[0])/(N-1);
		for(int i = 0; i < N; i++) {
			double z = x_b[0]+i*step2;
			double s1 = q1.evalute(z);
			double s2 = q2.evalute(z);
			double s3 = q3.evalute(z);

			qstream.WriteLine($"{z}	{s1}	{s2}	{s3}");

		}
		qstream.Close();
	
		WriteLine("The file linpol.svg contains a plot of the linear spline of a Sine function and its antiderivative.");
		WriteLine("====================================================================");
		WriteLine("The following are the results of the program calculating the c's and b's for my quadratic spline.");
		WriteLine("Additionally is a plot of the splines and the tables in the file qspline.svg");
		WriteLine("For the first table: y = 1 = c_i(x-x_i)² + b_i(x-x_i) + y_i");
	       	WriteLine("c	calc.	b	calc.	y	calc.");
		for(int i = 0; i < 4; i++) {
			WriteLine($"{q1.c[i]}	{0}	{q1.b[i]}	{0}	{q1.y[i]}	{y1[i]}");
		}
		WriteLine("For the second table: y = x = c_i(x-x_i)² + b_i(x-x_i) + y_i");
	       	WriteLine("c	calc.	b	calc.	y	calc.");
		for(int i = 0; i < 4; i++) {
			WriteLine($"{q2.c[i]}	{0}	{q2.b[i]}	{1}	{q2.y[i]}	{y2[i]}");
		}
		WriteLine("For the third table: y = x² = c_i(x-x_i)² + b_i(x-x_i) + y_i");
	       	WriteLine("c	calc.	b	calc.	y	calc.");
		for(int i = 0; i < 4; i++) {
			WriteLine($"{q3.c[i]}	{1}	{q3.b[i]}	{2*x_b[i]}	{q3.y[i]}	{y3[i]}");
		}
	}

}

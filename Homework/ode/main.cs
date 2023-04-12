using System;
using System.IO;
using static System.Console;
using static System.Math;

class main {
	
	static Func<double, vector, vector> F1 = delegate(double x, vector y) {
		return new vector (y[1], -y[0]);
	};
	static Func<double, vector, vector> F2 = delegate(double x, vector y) {
		double b = 0.25; double c = 5;
		return new vector (y[1], -b*y[1]-c*Sin(y[0]));
	};
	static Func<double, vector, vector> F3 = delegate(double x, vector y) {
		double a = 1.5, b = 1, c = 3, d = 1;
		return new vector (a*y[0] - b*y[0]*y[1], -c*y[1] + d*y[0]*y[1]);
	};

	static void Main() {
		var xs = new genlist<double>();
		var ys = new genlist<vector>();

		/* I want to solve u'' = - u from 0 to 4 pi. */
		var diffeq1 = new StreamWriter("diffeq1.txt");
		vector y = ode.driver(F1, 0, new vector(0, 1), 4*PI, xs, ys);
		for(int i = 0; i < xs.size; i++) {
			diffeq1.WriteLine($"{xs[i]}	{ys[i][0]}	{ys[i][1]}");
		}
		diffeq1.Close();
	
		xs = new genlist<double>();
		ys = new genlist<vector>();

		/* I want to solve o''+b*o'+c*sin(o) = 0 from 0 to 10. */
		var diffeq2 = new StreamWriter("diffeq2.txt");
		vector y2 = ode.driver(F2, 0, new vector(PI-0.1, 0), 10, xs, ys);
		for(int i = 0; i < xs.size; i++) {
			diffeq2.WriteLine($"{xs[i]}	{ys[i][0]}	{ys[i][1]}");
		}
		diffeq2.Close();

		/* I want to solve the Lotka-Volterra equations from t = 0 to 15 and with 
		 * boundary conditions x(0) = 10 and y(0) = 5. */ 
		xs = new genlist<double>();
		ys = new genlist<vector>();
		var diffeq3 = new StreamWriter("diffeq3.txt");
		vector y3 = ode.driver(F3, 0, new vector(10, 5), 15, xs, ys);
		for(int i = 0; i < xs.size; i++) {
			diffeq3.WriteLine($"{xs[i]}	{ys[i][0]}	{ys[i][1]}");
		}
		diffeq3.Close();

	}
	

}



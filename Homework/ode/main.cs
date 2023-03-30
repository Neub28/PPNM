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

	static void Main() {
		/* I want to solve u'' = - u from 0 to 4 pi. */
		var diffeq1 = new StreamWriter("diffeq1.txt");
		var (xs, ys) = ode.driver(F1, 0, new vector(0, 1), 4*PI);
		for(int i = 0; i < xs.size; i++) {
			diffeq1.WriteLine($"{xs[i]}	{ys[i][0]}	{ys[i][1]}");
		}
		diffeq1.Close();

		/* I want to solve o''+b*o'+c*sin(o) = 0 from 0 to 10. */
		var diffeq2 = new StreamWriter("diffeq2.txt");
		var (xs2, ys2) = ode.driver(F2, 0, new vector(PI-0.1, 0), 10);
		for(int i = 0; i < xs2.size; i++) {
			diffeq2.WriteLine($"{xs2[i]}	{ys2[i][0]}	{ys2[i][1]}");
		}
		diffeq2.Close();

	}
	

}



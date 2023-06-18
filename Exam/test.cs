using System;

class main {

	public static void Main() {
		Func<double, double, double> f = (x,y) => { return x+y; };
		System.Console.WriteLine($"{f(1)}");

	}

}

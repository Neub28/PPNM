using static System.Console;
using static System.Math;

class main{
	static void Main() {
		findMaxInt();
		findMinInt();
		findDoubleEps();
		findFloatEps();

		int n = (int) 1e6;
		double epsilon = Pow(2, -52);
		double tiny = epsilon/2;
		double sumA=0;
		double sumB=0;

		sumA += 1;
		for(int i = 0; i < n; i++) {sumA+=tiny;}
		Write($"sumA - 1 = {sumA - 1}, but should be {n*tiny}\n");

		for(int i = 0; i < n; i ++) {sumB+=tiny;}
		sumB += 1;
		Write($"sumB - 1 = {sumB - 1}, but should be {n*tiny}\n");
		
		Write($"Using approx-function on sumA: returns {approx(sumA-1, n*tiny)}\n");		
		Write($"Using approx-function on sumB: returns {approx(sumB-1, n*tiny)}\n");
						
	}
	static void findMaxInt() {
		int i = 1;
		while(i+1 > i) { i++; }
		Write("My max int is {0}\n", i);
		Write($"MaxValue method gives {int.MaxValue}\n");
	}
	static void findMinInt() {
		int i = 1;
		while(i-1 < i) { i--; }
		Write("My min int is {0}\n", i);
		Write($"MinValue method gives {int.MinValue}\n");
	}
	static void findDoubleEps() {
		double x = 1;
		while(1 + x != 1) {x /= 2;}
		x *= 2;
		Write("Epsilon for type double is {0}\n", x);
		Write($"Should be: {Pow(2, -52)}\n");
	}
	static void findFloatEps() {
		float y = 1F;
		while((float) (1F +y) != 1F) {y/=2F;}
		y *= 2F;
		Write("Epsilon for type float is {0}\n", y);
		Write($"Should be: {Pow(2, -23)}\n");
	}
	static bool approx(double a, double b, double tau=1e-9, double epsilon=1e-9) {
		if(Abs(a-b) < tau) { return true;  }
		if(Abs(a-b)/(Abs(a)+Abs(b)) < epsilon) { return true; }
		else return false;
		
	}
}

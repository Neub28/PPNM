using System;
using static System.Math;
using static System.Console;
class main{
	static double sqrt2 = Sqrt(2.0);
	static double pow2 = Pow(2.0, (double)  1/5);
	static double exp = Exp(PI);
	static double pi = Pow(PI, Exp(1));
	
	public static void Main() {
	Write($"sqrt(2) = {sqrt2} (should be 2)\n");
	Write($"2^1/5 = {pow2}\n");
	Write($"e^pi = {exp}\n");
	Write($"pi^e = {pi}\n");
	Write($"g(1) = {sfuns.gamma(1.0)}, g(2) = {sfuns.gamma(2.0)}, g(3) = {sfuns.gamma(3.0)}, g(10) = {sfuns.gamma(10.0)}\n"); 
	}
}

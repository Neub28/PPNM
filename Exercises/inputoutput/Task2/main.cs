using System;
using static System.Console;
using static System.Math;

class main {
	public static void Main() {
		char[] delimiters = {'	', '\t','\n', ' '};
		for(string line = ReadLine(); line != null; line = ReadLine() ){
			var numbers = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
			foreach(var number in numbers){
				double x = double.Parse(number);
				WriteLine($"{x}, {Sin(x)}, {Cos(x)}");
			}
		}
				
	}
}

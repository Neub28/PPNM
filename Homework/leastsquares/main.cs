using System;
using System.IO;
using static System.Math;
using static System.Console;

class main {
	public static vector t, y, logy, dy;
	static void Main() {
		
		for(string s = ReadLine(); s != null; s = ReadLine()) {
			var words = s.Split(":");
			WriteLine($"{words[0]}");
			if(words[0] == "t") {
				var numbers = words[1].Split(",");
				t = new vector(numbers.Length);
				WriteLine("T");
				for(int i = 0; i < numbers.Length; i++) {
					t[i] = double.Parse(numbers[i]);
				}
				WriteLine("T DONE");
			}

			if(words[0] == "y") {
				var numbers = words[1].Split(",");
				WriteLine("Y");
				y = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					y[i] = double.Parse((numbers[i]));
				}
				WriteLine("Y DONE");

			}
		
			if(words[0] == "dy") {
				var numbers = words[1].Split(",");
				WriteLine("DY");
				dy = new vector(numbers.Length);
				WriteLine($"{numbers.Length}	{y.size}");
				for(int i = 0; i < numbers.Length; i++) {
					double val = double.Parse(numbers[i]);
					dy[i] = val/y[i];
				}
				WriteLine("DY DONE");
			}
		}
		WriteLine("Passed ");
		logy = new vector(y.size);
		for(int i = 0; i < t.size; i++) {
			logy[i] = Log(y[i]);
		}

		var fs = new Func<double, double>[] {z => z};
		var outstream = new StreamWriter("data_table.txt");
		for(int i = 0; i < t.size; i++) {
			outstream.WriteLine($"{t[i]}	{logy[i]}	{dy[i]}");
		}
		outstream.Close();
		vector c = leastsquares.lsfit(fs, t, logy, dy);
		
		var outstream2 = new StreamWriter("fit_parameters.txt");
		for(int i = 0; i < c.size; i++) { outstream2.WriteLine($"{i}	{c[i]}"); }
		outstream2.Close();
		
		WriteLine($"Fitted value of k = {c[0]}, which gives a half-life time of {Log(2)/c[0]} seconds"); 
	}				
}


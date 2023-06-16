using System;
using System.IO;
using static System.Math;
using static System.Console;

class main {
	public static vector t, y, logy, dy, dyb;
	static void Main() {
		
		for(string s = ReadLine(); s != null; s = ReadLine()) {
			var words = s.Split(":");
			if(words[0] == "t") {
				var numbers = words[1].Split(",");
				t = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					t[i] = double.Parse(numbers[i]);
				}
			}

			if(words[0] == "y") {
				var numbers = words[1].Split(",");
				y = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					y[i] = double.Parse((numbers[i]));
				}

			}
		
			if(words[0] == "dy") {
				var numbers = words[1].Split(",");
				dy = new vector(numbers.Length);
				dyb = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					double val = double.Parse(numbers[i]);
					dy[i] = val/y[i];
					dyb[i] = val;
				}
			}
		}
		logy = new vector(y.size);
		for(int i = 0; i < t.size; i++) {
			logy[i] = Log(y[i]);
		}

		var fs = new Func<double, double>[] {z => 1.0, z => z};
		var outstream = new StreamWriter("data_table.txt");
		for(int i = 0; i < t.size; i++) {
			outstream.WriteLine($"{t[i]}	{logy[i]}	{dy[i]}	{y[i]}	{dyb[i]}");
		}
		outstream.Close();
		var cS = leastsquares.lsfit(fs, t, logy, dy);
		vector c = cS.Item1;
		matrix cov = cS.Item2;

		var outstream2 = new StreamWriter("fit_parameters.txt");
		outstream2.Write($"{Exp(c[0])} {c[1]} {Exp(Sqrt(cov[0][0]))} {Sqrt(cov[1][1])}");
		outstream2.WriteLine("");
		outstream2.Close();
		
		WriteLine($"Fitted value of k = {c[1]}, which gives a half-life time of {-Log(2)/c[1]} days");
		WriteLine($"The uncertainty of k is: {Sqrt(cov[1][1])}, therefore the uncertainty in the half time is {Abs(-Log(2)/(c[1]+Sqrt(cov[1][1]))-(-Log(2)/c[1]))} ");
		WriteLine("This value is now known to be: 3.66 days (http://nucleardata.nuclear.lu.se/toi/nuclide.asp?iZA=880224)");	
		WriteLine($"Therefore Rutherford and Soddys half-life time value is not in agreement with the modern value within the estimated uncertainty.");
	}				
}


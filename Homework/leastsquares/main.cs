using System;
using System.IO;
using static System.Math;
using static System.Console;

class main {
	public static vector t, y, logy, dy;
	static void Main() {
		
		for(string s = ReadLine(); s != null; s = ReadLine()) {
			var words = s.Split(":");
			//WriteLine($"{words[0]}");
			if(words[0] == "t") {
				var numbers = words[1].Split(",");
				t = new vector(numbers.Length);
				//WriteLine("T");
				for(int i = 0; i < numbers.Length; i++) {
					t[i] = double.Parse(numbers[i]);
				}
				//WriteLine("T DONE");
			}

			if(words[0] == "y") {
				var numbers = words[1].Split(",");
				//WriteLine("Y");
				y = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					y[i] = double.Parse((numbers[i]));
				}
				//WriteLine("Y DONE");

			}
		
			if(words[0] == "dy") {
				var numbers = words[1].Split(",");
				//WriteLine("DY");
				dy = new vector(numbers.Length);
				for(int i = 0; i < numbers.Length; i++) {
					double val = double.Parse(numbers[i]);
					dy[i] = val/y[i];
				}
				//WriteLine("DY DONE");
			}
		}
		//WriteLine("Passed ");
		logy = new vector(y.size);
		for(int i = 0; i < t.size; i++) {
			logy[i] = Log(y[i]);
		}

		var fs = new Func<double, double>[] {z => 1.0, z => z};
		var outstream = new StreamWriter("data_table.txt");
		for(int i = 0; i < t.size; i++) {
			outstream.WriteLine($"{t[i]}	{logy[i]}	{dy[i]}");
		}
		outstream.Close();
		var cS = leastsquares.lsfit(fs, t, logy, dy);
		//WriteLine($"{cS.GetType()}");
		vector c = cS.Item1;
		matrix cov = cS.Item2;

		var outstream2 = new StreamWriter("fit_parameters.txt");
		for(int i = 0; i < c.size; i++) { outstream2.WriteLine($"{i}	{c[i]}"); }
		outstream2.Close();
		
		WriteLine($"Fitted value of k = {c[1]}, which gives a half-life time of {-Log(2)/c[1]} days");
		WriteLine($"The uncertainty of k is: {cov[1][1]}, therefore the uncertainty in the half time is {Abs(-Log(2)/(c[1]+cov[1][1])-(-Log(2)/c[1]))} ");
		WriteLine("This value is now known to be: 3.66 days (http://nucleardata.nuclear.lu.se/toi/nuclide.asp?iZA=880224)");	
		WriteLine($"Therefore Rutherford and Soddys half-life time value is not in agreement with the modern value within the estimated uncertainty.");
	}				
}


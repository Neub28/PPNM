using System;
using System.IO;
using System.Linq;
using static System.Math;
using static System.Console;

public class make_data {
	public static void Main(string[] args) {
		var outstream = new StreamWriter("data.txt");
		foreach(var arg in args) {
			var words = arg.Split(':');
			if(words[0] == "-t") {
				var numbers = words[1].Split(',');
				outstream.Write("t:");
				int i = 1;
				foreach(var number in numbers) {
					double t = double.Parse(number);
					if(i == numbers.Length) {
						outstream.Write($"{t}");
					}
					else
						outstream.Write($"{t},");
						i++;
				}
				outstream.WriteLine("");
			}
			if(words[0] == "-y") {
				var numbers = words[1].Split(',');
				outstream.Write("y:");
				int i = 1;
				foreach(var number in numbers) {
					double y = double.Parse(number);
					if(i == numbers.Length) {
						outstream.Write($"{y}");
					}
					else 
						outstream.Write($"{y},");
						i++;
				}
				outstream.WriteLine("");
			}
			if(words[0] == "-dy") {
				var numbers = words[1].Split(',');
				outstream.Write("dy:");
				int i = 1;
				foreach(var number in numbers) {
					double dy = double.Parse(number);
					if(i == numbers.Length) {
						outstream.Write($"{dy}");
					}
					else
						outstream.Write($"{dy},");
						i++;
				}
				outstream.WriteLine("");
			}
		}
		outstream.Close();
	}
}

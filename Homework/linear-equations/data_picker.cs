using System;
using static System.Console;

public class data_picker {
	public static double n, user_time;
	
	public static void Main() {
	char[] delimiters = {' ','\t','\n'};
	var split_options = StringSplitOptions.RemoveEmptyEntries;

	for(string line = ReadLine(); line != null; line = ReadLine()) {
		var words = line.Split(delimiters, split_options);
		if(words[0] == "n") { 
			n = double.Parse(words[1]);	
		}
		if(words[0] == "user") {
			user_time = double.Parse(words[1]);
			WriteLine($"{n}	{user_time}");
		}	
	}
	}
}

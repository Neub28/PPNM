using static System.Console;

class main{
static void Main(string[] args) {
	// Seperated the Main into taking an input handling the different 
	// functions
	
	if(args[0] == "-erf") {
	var outstream = new System.IO.StreamWriter("error_func.data");
	for(double x = -5+1.0/128; x <= 5; x += 1.0/128) {
		outstream.WriteLine($"{x} {sfuns.erf(x)}");
	}
	outstream.Close();
	}

	if(args[0] == "-gamma") {
	var outstream = new System.IO.StreamWriter("gamma.data");
	for(double x = -5+1.0/128; x <= 5; x += 1.0/128) {
		outstream.WriteLine($"{x} {sfuns.gamma(x)}");
	}
	outstream.Close();
	}

	if(args[0] == "-lngamma") {
	var outstream = new System.IO.StreamWriter("lngamma.data");
	for(double x = 1.0/128 ; x <= 12; x += 1.0/128) {
		outstream.WriteLine($"{x} {sfuns.lngamma(x)}");
	}
	outstream.Close();
	}
	
	}
}

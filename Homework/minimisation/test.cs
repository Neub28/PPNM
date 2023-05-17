using System;
using static System.Console;

class main {
	static void Main() {
		vector v1 = new vector(1, 1, 1);
		double step = 0.5;
		vector[] vs = new vector[4];
		vs[v1.size] = v1.copy();
		for(int i = 0; i < v1.size; i++) {
			v1[i] += step;
			vs[i] = v1.copy();
			v1[i] -= step;
		}
		foreach(vector v in vs) {
			Error.WriteLine($"{v[0]}  {v[1]}   {v[2]}");
		}
		Error.WriteLine($"{vs[3]}");

	}

}

using static System.Console;
using static System.Math;

class main {
	public static void Main() {
		vec v1 = new vec(1, 2, 3);
		vec v2 = new vec(3, 2, 1);
		
		WriteLine("I initialized two vectors:");
		v1.print();
		v2.print();
		WriteLine("Cross product yields:");
		vec v3 = v1.vecproduct(v2);
		v3.print();
		WriteLine("Dot-product between crossed vector and first vector:");
		var a = v3.dot(v1);
		WriteLine(a);
		
	}
}

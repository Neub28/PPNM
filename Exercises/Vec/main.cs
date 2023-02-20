using static System.Console;
using static System.Math;

public class main {
	public static void Main() {
		vec v1 = new vec(1, 2, 3);
		vec v2 = new vec(3, 2, 1);

		vec v3 = vec1.vecproduct(vec2);
		vec3.print();

		var a = vec3.dot(vec1);
		WriteLine(a);
		
	}
}

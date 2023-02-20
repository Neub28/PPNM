using static System.Console;
using static System.Math;

public class vec {
	public double x,y,z;
	/* Constructor for given parameters  */
	public vec(double a, double b, double c) { x=a; y=b; z=c; }
	
	/* Constructor for no given parameters */
	public vec() { x=y=z=0; }

	/* Overriding the ToString method */
	public override string ToString() {
		return $"{x} {y} {z}";
	}

	/* Method printing the coordinates of the vector */
	public void print(string s) {Write(s); WriteLine($"(x = {x}, y = {y}, z = {z}"); }
	public void print(){this.print("");}

	/* Addition operator for vectors */
	public static vec operator+(vec u, vec v) { 
		return new vec(u.x+v.x, u.y+v.y, u.z+v.z);		
	}

	/* Subtraction operator for vectors  */
	public static vec operator-(vec u, vec v) {
		return new vec(u.x-v.x, u.y-v.y, u.z-v.z);
	}
	/* Defining the negative of a vector  */
	public static vec operator-(vec v) {
		return new vec(-v.x, -v.y, -v.z);
	}

	/* Multiplication of a vector with s scalar */
	public static vec operator*(vec u, double c) {
		return new vec(u.x*c, u.y*c, u.z*c);
	}

	/* Commutation between multiplication of a vector with a scalar */
	public static vec operator*(double c, vec u) {
		return u*c;
	}

	/* Dot product between two vectors  */
	public static double operator% (vec u, vec v){
		return u.x*v.x+u.y*v.y+u.z*v.z;
	}

	/* Method for dotting two vectors together */
	public double dot(vec other) { return this%other; }

	/* Method for doing the cross product of a vector  */
	public vec vecproduct(vec other) {
		double x_n = this.y*other.z-this.z*other.y;
		double y_n = this.z*other.x-this.x*other.z;
		double z_n = this.x*other.y-this.y*other.x;
		return new vec(x_n, y_n, z_n);
	}

	/* The length of the vector */
	public static double norm(vec u) {
		return Sqrt(Pow(u.x, 2) + Pow(u.y, 2) + Pow(u.z, 2));
	}
	
	/* Internal approx method  */ 	
	static bool approx(double a, double b, double acc=1e-9, double eps=1e-9) {
		if(Abs(a-b)<acc) return true;
		if( Abs(a-b)<(Abs(a)+Abs(b))*eps ) return true;
		return false;
	}
	/* Determine whether two vectors are approximately equal */
	public bool approx(vec other) {
		if(!approx(this.x, other.x)) return false;
		if(!approx(this.y, other.y)) return false;
		if(!approx(this.z, other.z)) return false;
		return true;
	}
}


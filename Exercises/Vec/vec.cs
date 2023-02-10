using static System.Console;
public class vec {
	public double x,y,z;
	public vec(double a, double b, double c) { x=a; y=c; y=c; }
	public vec() { x=y=z=0; }
	public void print(string s) {Write(s); WriteLine($"(x = {x}, y = {y}, z = {z}"); }
	public static vec operator+(vec u, vec v) { 
		return new vec(u.x+v.x, u.y+v.y, u.z+v.z);		
	}
	public static vec operator-(vec u, vec v) {
		return new vec(u.x-v.x, u.y-v.y, u.z-v.z);
	}
	public static vec operator-(vec v) {
		return new vec(-v.x, -v.y, -v.z);
	}
	public static vec operator*(vec u, double c) {
		return new vec(u.x*c, u.y*c, u.z*c);
	}
	public static vec operator*(double c, vec u) {
		return u*c;
	}
	public static double operator% (vec u, vec v){ /* Dot product */
		return u.x*v.x+u.y*v.y+u.z*v.z;
	}
	public double dot(vec other) { return this%other; }
}


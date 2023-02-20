using System;
using static System.Console;
using static System.Math;

class main{
	public static void Main() {
		/* Define the constant i  */
		complex i = new complex(0,1);

		/* Is sqrt(-1) = + or - 1? */
		complex sqrt_minus_one = cmath.sqrt(new complex(-1,0));
		bool equal_a = sqrt_minus_one.approx(i);
		WriteLine($"{sqrt_minus_one} = {i}: {equal_a}");

		/* Is sqrt(i) = 1/sqrt(2) + i/sqrt(2) ? */
		complex sqrt_i = cmath.sqrt(i);
		complex b = new complex(1/Pow(2.0, 0.5), 1/Pow(2.0, 0.5));
		bool equal_b = sqrt_i.approx(b);
		WriteLine($"{sqrt_i} = {b}: {equal_b}");

		/* Is e^i = cos(1) + i*sin(1) ?  */
		complex e_i = cmath.exp(i);
		complex c = new complex(Cos(1), Sin(1));
		bool equal_c = e_i.approx(c);
		WriteLine($"{e_i} = {c}: {equal_c}");
		
		/* Is e^i*pi = -1 ? */
		complex e_ipi = cmath.exp(i*PI);
		complex d = new complex(-1, 0);
		bool equal_d = e_ipi.approx(d);
		WriteLine($"{e_ipi} = {d}: {equal_d}");

		/* Is i^i = e^-pi/2 ? */
		complex i_i = cmath.pow(i, i);
		complex e = new complex(Exp(-PI/2), 0);
		bool equal_e = i_i.approx(e);
		WriteLine($"{i_i} = {e}: {equal_e}");
			 		
		/* Is sinh(i) = i*sin(1) ?  */
		complex sinh_i = cmath.sinh(i);
		complex f = i*Sin(1);
		bool equal_f = sinh_i.approx(f);
		WriteLine($"{sinh_i} = {f}: {equal_f}");

		/* Is cosh(i) = cos(1) ? */
		complex cosh_i = cmath.cosh(i);
		bool equal_g = cosh_i.approx(new complex(Cos(1), 0));
		WriteLine($"{cosh_i} = {Cos(1)}: {equal_g}");

		/* Is ln(i) = i pi/2 ? */ 
		complex ln_i = cmath.log(i);
		complex h = i*PI/2;
		bool equal_h = ln_i.approx(h);
		WriteLine($"{ln_i} = {h}: {equal_h}");

		/* Is sin(i pi) = i*sinh(pi) ? */ 
		complex sin_ipi = cmath.sin(i*PI);
		complex i1 = i * Sinh(PI);
		bool equal_i = sin_ipi.approx(i1);
		WriteLine($"{sin_ipi} = {i1}: {equal_i}");
		
	}
}

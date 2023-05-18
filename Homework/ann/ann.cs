using System;
using static System.Console;
using static System.Math;


public class ann {
	public readonly int n; 		/* number of neurons */
	public int counts;		/* counts of operations */
	public Func<double, double> f = x => x*Exp(-Pow(x,2));
	public Random random = new Random();
	public vector p; 
	
	/* Methods for setting values in parameter vector p. */
	public void seta(int i, double z) { p[i] = z; }
	public void setb(int i, double z) { p[i+n] = z; }
	public void setw(int i, double z) { p[i+2*n] = z; }
	/* Methods for getting values in parameter vector input p. */
	public double a(int i) { return p[i]; }
	public double b(int i) { return p[i+n]; }
	public double w(int i) { return p[i+2*n]; }
	public double getRnd() { return random.NextDouble()-0.5; }
	
	/* Constructor  */
	public ann(int n) {	
		this.n = n; 
		p = new vector(3*n);
	}
	public ann(vector ps) {
		this.n = ps.size/3;
		this.p = ps;
	}

	/* Returns network response, denoted Fp(x). */

	public double response(double x) {
	double sum = 0;
	/* Sum over all neurons */
	for(int i = 0; i < n; i++) {
		sum += w(i)*f((x-a(i))/b(i));
	}
	return sum;
	}

	public void train(double[] x, double[] y, string method = null, double precision = 1e-4, double step=0.5) {
	if(x.Length != y.Length) throw new ArgumentException("Input arrays are not equal in length.");
	if(method != "qnewton" && method != "simplex") { 
		Error.WriteLine("Method was not recognized. Inputs qnewton and simplex are only allowed.");
		Error.WriteLine("Training stopped.");
		return;
		}
	Error.WriteLine("Training in progress.....");
	
	for(int i = 0; i < n; i++) {
		setw(i,1);
		setb(i,1);
		seta(i,x[0]+(x[x.Length-1]-x[0])*i/(n-1)); 
	}

	Func<vector, double> Cp = delegate(vector ps) {
		double sum = 0;
		ann NN = new ann(ps);
		for(int i = 0; i < x.Length; i++) {
			sum += Pow(NN.response(x[i]-y[i]), 2);
		}
		return sum/x.Length;
	};

	/* Use minimisation routine */
	if(method == "qnewton") {
		var (ptrained, operations) = minimisation.qnewton(Cp, this.p, precision);
		this.p = ptrained;
		this.counts = operations;
		}
	if(method == "simplex") {
		if(step == null) { Error.WriteLine("No step size given. Returning..."); return; }
		var (ptrained, operations) = minimisation.simplex(Cp, this.p, precision, step);
		this.p = ptrained;
		this.counts = operations;
	}
	Error.WriteLine($"Training succesfull with: {this.counts} tries.");
	}


}



using System;
using static System.Console;
using static System.Math;


public class ann {
	public readonly int n; 		/* number of neurons */
	public int counts;		/* counts of operations */
	public Func<double, double> f = x => x*Exp(-Pow(x,2));
	public Func<double, double> deriv = x => Exp(-Pow(x,2))-2*Exp(-Pow(x,2))*Pow(x,2);
	public Func<double, double> dderiv = x => 4*Exp(-Pow(x,2))*Pow(x,3)-6*Exp(-Pow(x,2))*x;
	public Func<double, double> antideriv = x => -0.5*Exp(-Pow(x,2));
	public vector p; 
	
	/* Methods for setting values in parameter vector p. */
	public void seta(int i, double z) { this.p[i] = z; }
	public void setb(int i, double z) { this.p[i+n] = z; }
	public void setw(int i, double z) { this.p[i+2*n] = z; }
	/* Methods for getting values in parameter vector input p. */
	public double a(int i) { return this.p[i]; }
	public double b(int i) { return this.p[i+n]; }
	public double w(int i) { return this.p[i+2*n]; }
	
	/* Constructor  */
	public ann(int n) {
		this.n = n; 
		p = new vector(3*n);

	}
	public ann(vector ps) {
		this.n = ps.size/3;
		this.p = ps;
	}

	public double derivativeResponse(double x) {
		double sum = 0;
		for(int i = 0; i < n; i++) {
			/* x' -> x-a/b gives factor 1/b*/
			sum += w(i)/b(i)*deriv((x-a(i))/b(i));
		}
		return sum;

	}

	public double dderivativeResponse(double x) {
		double sum = 0; 
		for(int i = 0; i < n; i++) {
			/* x' -> x-a/b gives factor 1/b²*/
			sum += w(i)/Pow(b(i),2)*dderiv((x-a(i))/b(i));
		}
		return sum; 

	}
	
	public double antiderivativeResponse(double x) {
		double sum = 0; 
		for(int i = 0; i < n; i++) {
			/* Substitution from x' -> x-a/b gives factor b */
			sum += w(i)*b(i)*antideriv((x-a(i))/b(i));
		}
		return sum;
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
	Func<vector,double> cost = (u) => {
		ann annu = new ann(u);
		double sum=0;
		for(int k=0;k<x.Length;k++)
			sum+=Pow(annu.response(x[k])-y[k],2);
		return sum/x.Length;
		};

	/* Use minimisation routine */
	if(method == "qnewton") {
		var (ptrained, operations) = minimisation.qnewton(cost, this.p, precision);
		this.p = ptrained;
		this.counts = operations;
		}
	if(method == "simplex") {
		var (ptrained, operations) = minimisation.simplex(cost, this.p, precision, step);
		this.p = ptrained;
		this.counts = operations;
	}
	Error.WriteLine($"Training succesfull with: {this.counts} tries.");
	}
	
	/* Method does not define cost function for user, but only takes as argument. */
	public void diffeqTrain(Func<vector, double> diffeq, double a, double b, 
	double x0, double y0, double y0p, double alpha = 1, double beta = 1, double precision = 1e-3, double step = 0.5) {
	
	/* Used for initial guess. a's should be evenly spaced across range */
	for(int i = 0; i < n; i++) {
		setw(i,1);
		setb(i,1);
		seta(i,a+(b-a)*i/(n-1)); 
	}
	/* Cost function */
	Func<vector, double> cost = ps => {
		
		ann annu = new ann(ps);
		
		// Define integral function needed by numerical integration routine. 
		Func<double, double> integral = x => {
			// input = [ φ'', φ', φ, t ]
			vector input = new vector(4);
			input[0] = annu.dderivativeResponse(x);
			input[1] = annu.derivativeResponse(x);
			input[2] = annu.response(x);
			input[3] = x;
			return diffeq(input)*diffeq(input);
		};
		
		double finalsum = 0; 
		finalsum += integrator.integrate(integral, a, b, 1e-3, 1e-3);
		finalsum += alpha*Pow(annu.response(x0)-y0, 2);
		finalsum += beta*Pow(annu.derivativeResponse(x0)-y0p, 2);
		
		return finalsum;
	};
	
	Error.WriteLine("Unsupervised training.....");
	
	var (ptrained, operations) = minimisation.simplex(cost, this.p, precision, step);
	this.p = ptrained;
	this.counts = operations;

	Error.WriteLine($"Unsupervised training succesfull with: {this.counts} tries.");
	
	}


}



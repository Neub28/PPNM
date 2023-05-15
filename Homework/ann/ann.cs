using System;
using static System.Console;
using static System.Math;


public class ann {
	public int n; /* number of neurons */
	public int lastTries;
	public Func<double, double> f = x => x*Exp(-Pow(x,2));
	public Random random = new Random();
	public vector p; 
	
	/* Constructor  */
	public ann(int n) {	
		this.n = n;
		this.lastTries = 0;
		double[] ar = new double[n*3];
		for(int i = 0; i < (n*3); i++) {
			ar[i]= (double) random.NextDouble()*1-0.5;
		}
		this.p = new vector(ar);

	}
	/* Returns network response, denoted Fp(x). */
	public double response(double x) {
	double Fp = 0;
	/* Sum over all neurons */
	for(int i = 0; i < n; i++) {
		/* i'th neuron has ai, bi & wi in vector p */
		double ai = p[i*3];
		double bi = p[i*3+1];
		double wi = p[i*3+2];
		Fp += f((x-ai)/bi)*wi;
	}
	return Fp;
	}

	public void train(vector x, vector y) {
	if(x.size != y.size) throw new ArgumentException("Input arrays are not equal in length.");
	Error.WriteLine("Training in progress.....");
	Func<vector, double> Cp = delegate(vector ps) {
		double Cpsum = 0;
		for(int i = 0; i < x.size; i++) {
			/* Function sum F_p(x_i) */
			double Fpxi = 0;
			/* Forloop over all parameters possible in vector input */
			for(int ni = 0; ni < (this.n-1) ; ni++) {
				double ai = ps[ni*3];
				double bi = ps[ni*3+1];
				double wi = ps[ni*3+2];
				Fpxi += f((x[i]-ai)/bi)*wi;
			}

			Cpsum += Pow(Fpxi-y[i],2);
		}
		return Cpsum;
	};
	/* Use Quasi-Newton minimisation routine */
	var res = minimisation.qnewton(Cp, this.p, 1e-3);
	/* Save values of minimisation */
	this.p = res.Item1;
	Error.WriteLine($"Training succesfull with: {res.Item2} tries.");
	}


}



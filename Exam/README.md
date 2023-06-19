Student number: 2021092**60**
Therefore the exam project is: **8** = 60 % 26: 

Adaptive integrator
-------------------
Implement a two-dimensional integrator for integrals in the form

![name](2dintegral.png)

which applies your favourite adaptive one-dimensional integrator along each of the two dimensions. The signature might be something like

static double integ2D(
	Func<double,double,double> f,
	double a, double b,
	Func<double,double> d,
	Func<double,double> u,
	double acc, double eps)

-------------------



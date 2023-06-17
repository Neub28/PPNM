import numpy as np
from scipy.integrate import quad

# First I integrate 1/sqrt(x) from 0 to 1

count1 = 0
def f1(x) : 
	global count1
	count1 += 1
	return 1/np.sqrt(x)

i1 = quad(f1, 0, 1, epsabs=0.001, epsrel=0.001)
print("Integral of 1/sqrt(x) from 0 to 1 is:")
print("Result is: ", i1, " calculated with ", count1, " calls")

# Next I integrate ln(x)/sqrt(x) from 0 to 1

count2 = 0
def f2(x) :
	global count2
	count2 += 1
	return np.log(x)/np.sqrt(x)

i2 = quad(f2, 0, 1, epsabs=0.001, epsrel=0.001)
print("Integral of log(x)/sqrt(x) from 0 to 1 is:")
print("Result is: ", i2, " calculated with ", count2, " calls")

count2 = 0
def f3(x) : 
	global count2
	count2 += 1
	return 1/(x*x)

i3 = quad(f3, 1, np.inf, epsabs=0.001, epsrel=0.001)
print("Integral of 1/x² from 1 to inf is:")
print("Result is: ", i3, " calculated with", count2, " calls")

count2 = 0;
def f4(x) : 
	global count2
	count2 += 1
	return x*np.exp(-x*x)

i4 = quad(f4, -np.inf, 0, epsabs=0.001, epsrel=0.001)
print("Integral of x*exp(-x²) from -inf to 0 is:")
print("Result is: ", i4, " calculated with", count2, " calls")

count2 = 0;
def f5(x) : 
	global count2
	count2 += 1
	return 1/(1+x*x)

i5 = quad(f5, -np.inf, np.inf, epsabs=0.001, epsrel=0.001)
print("Integral of 1/(1+x²) from -inf to inf is:")
print("Result is: ", i5, " calculated with", count2, " calls")




 

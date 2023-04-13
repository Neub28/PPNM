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
 

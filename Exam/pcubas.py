import numpy as np
from scipy.integrate import dblquad

cs = []
ins = []
es = []

e = 1e-5
r = 1e-5

print("\n")

c = 0
c1 = 0
c2 = 0
def f1(y,x) : 
	global c
	c += 1
	return 42*y*y-12*x
def f2(y,x) : 
	global c1
	c1 += 1
	return 2*y*x*x+9*y*y*y
def f3(y,x) : 
	global c2 
	c2 += 1 
	return x*(y-1)
def reset(integral, error, count) :
	ins.append(integral)
	es.append(error)
	cs.append(count)

(i,e) = dblquad(f1, 0, 4, lambda x: (x-2)**2, 6, epsabs=e, epsrel=r)
reset(i,e,c)
(i1,e1) = dblquad(f2, 0, 9, lambda x: 2*x/3, lambda x: np.sqrt(x)*2, epsabs=e, epsrel=r)
reset(i1,e1,c1)
(i2,e2) = dblquad(f3, -np.sqrt(2), np.sqrt(2), lambda x: x*x-3, lambda x: 1-x*x, epsabs=e, epsrel=r)
reset(i2,e2,c2)

print("Python completed these integrals with following results: ")
print("	Estimate	Error			Counts")
for integral, error, count in zip(ins, es, cs) :
	print('	{}		{}	{}'.format(integral,error,count))

print("\n")
print("Completion time:")




DR = 2 3 4 5 6 7 8 9 10
RMAX = 2 3 4 5 6 7 8 9 10

.PHONY: 
all : convergence.svg swavefunction.svg Out.txt

.PHONY:
open : 
	xdg-open convergence.svg
	xdg-open swavefunction.svg

convergence.svg : conv.gpi fixeddr.txt fixedrmax.txt
	gnuplot $<

swavefunction.svg : swave.gpi swave1.txt swave2.txt
	gnuplot $<

Out.txt fixeddr.txt fixedrmax.txt swave1.txt swave2.txt : main.exe
	mono main.exe -partA > Out.txt
	mono main.exe -partB > Out.txt
	rm -f fixeddr.txt fixedrmax.txt
	for i in $(RMAX) ; do \
		mono main.exe -rmax:$$i -dr:5 >> fixeddr.txt ; \
	done
	for i in $(DR) ; do \
		mono main.exe -rmax:5 -dr:$$i >> fixedrmax.txt ; \
	done
	mono main.exe -wavefunc:1 > swave1.txt
	mono main.exe -wavefunc:2 > swave2.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/vector.cs ../classes/matrix.cs jacobi_diag.cs
	mcs -target:library -out:$@ $^

.PHONY : clean
clean : 
	rm *.dll *.exe *.txt

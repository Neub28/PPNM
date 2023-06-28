DR = 2 3 4 5 6 7 8 9 10
RMAX = 2 3 4 5 6 7 8 9 10

all : Out.txt Swavefunction.svg Convergence.svg diagtimes.txt Times.svg

open : 
	xdg-open Convergence.svg
	xdg-open Swavefunction.svg
	xdg-open Times.svg

Swavefunction.svg : swave1.txt swave2.txt
	gnuplot swave.gpi

Convergence.svg : fixeddr.txt fixedrmax.txt
	gnuplot conv.gpi

Times.svg : times.gpi diagtimes.txt
	gnuplot times.gpi

Out.txt : main.exe
	mono main.exe -partA >> Out.txt
	mono main.exe -partB >> Out.txt
	mono main.exe -partC >> Out.txt

swave1.txt swave2.txt : main.exe 
	mono main.exe -wavefunc:1 > swave1.txt
	mono main.exe -wavefunc:2 > swave2.txt
	
fixeddr.txt fixedrmax.txt : main.exe
	$(MAKE) run_clean
	$(MAKE) run_parallel

diagtimes.txt : main.exe 
	mono main.exe -partCtest > diagtimes.txt

run_clean : 
	rm -f fixeddr.txt fixedrmax.txt

run_parallel : 
	for i in $(RMAX) ; do \
		mono main.exe -rmax:$$i -dr:5 >> fixeddr.txt ; \
	done & 
	for i in $(DR) ; do \
		mono main.exe -rmax:5 -dr:$$i >> fixedrmax.txt ; \
	done
	wait


main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/vector.cs ../classes/matrix.cs jacobi_diag.cs
	mcs -target:library -out:$@ $^

clean : 
	rm *.dll *.exe *.txt *.svg

.PHONY : 
all : Out.txt wavefunc.svg convergence.svg

convergence.svg : convergence.gpi acc.txt eps.txt rmin.txt rmax.txt
	gnuplot $<

wavefunc.svg : wavefunc.gpi wavefunc.txt
	gnuplot $<

Out.txt wavefunc.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs roots.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.svg *.txt *.exe *.dll

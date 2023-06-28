.PHONY : 
all : Out.txt Higgsfit.svg

.PHONY : 
open : 
	xdg-open Higgsfit.svg

Higgsfit.svg : higgs.gpi
	gnuplot $^

Out.txt bwfit.txt simplexfit.txt: main.exe
	mono main.exe < higgs.data > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs minimisation.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.txt *.exe *.dll *.svg

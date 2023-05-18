.PHONY : 
all : Out.txt higgsfit.svg

.PHONY : 
open : 
	xdg-open higgsfit.svg

higgsfit.svg : higgs.gpi
	gnuplot $^

Out.txt bwfit.txt simplexfit.txt: main.exe
	mono main.exe < Higgs.txt > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs minimisation.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.txt *.exe *.dll *.svg

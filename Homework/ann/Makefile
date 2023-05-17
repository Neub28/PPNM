.PHONY : 
all : Out.txt plotA.svg
	xdg-open plotA.svg

plotA.svg : plota.gpi
	gnuplot $<

Out.txt annvalues.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs nummethods.dll
	mcs main.cs -reference:nummethods.dll

nummethods.dll : ../classes/*.cs minimisation.cs ann.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.exe *.dll *.svg *.txt

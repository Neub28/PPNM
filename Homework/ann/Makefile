.PHONY : 
all : plotA.svg plotB.svg Out.txt

.PHONY :
open : 
	xdg-open plotA.svg
	xdg-open plotB.svg

plotA.svg : plota.gpi annvalues.txt simvalues.txt trainingpoints.txt
	gnuplot $<

plotB.svg : plotb.gpi trainingpointsB.txt annderivs.txt derivpoints.txt
	gnuplot $<

Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs nummethods.dll
	mcs main.cs -reference:nummethods.dll

nummethods.dll : ../classes/*.cs minimisation.cs ann.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.exe *.dll *.svg *.txt

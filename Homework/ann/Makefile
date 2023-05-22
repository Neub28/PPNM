.PHONY : 
all : plotA.svg plotB.svg plotC.svg Out.txt

.PHONY :
open : 
	xdg-open plotA.svg
	xdg-open plotB.svg
	xdg-open plotC.svg

plotA.svg : plota.gpi annvalues.txt simvalues.txt trainingpoints.txt
	gnuplot $<

plotB.svg : plotb.gpi trainingpointsB.txt annderivs.txt derivpoints.txt
	gnuplot $<

plotC.svg : plotc.gpi diffeq.txt
	gnuplot $<

Out.txt diffeq.txt trainingpoints.txt trainingspointsB.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs nummethods.dll
	mcs main.cs -reference:nummethods.dll

nummethods.dll : ../classes/*.cs minimisation.cs integrator.cs ann.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.exe *.dll *.svg *.txt

.PHONY : 
all : PlotA.svg PlotB.svg PlotC.svg Out.txt

.PHONY :
open : 
	xdg-open PlotA.svg
	xdg-open PlotB.svg
	xdg-open PlotC.svg

PlotA.svg : plota.gpi annvalues.txt simvalues.txt trainingpoints.txt
	gnuplot $<

PlotB.svg : plotb.gpi trainingpointsB.txt annderivs.txt derivpoints.txt
	gnuplot $<

PlotC.svg : plotc.gpi diffeq.txt
	gnuplot $<

Out.txt diffeq.txt trainingpoints.txt trainingspointsB.txt annvalues.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs nummethods.dll
	mcs main.cs -reference:nummethods.dll

nummethods.dll : ../classes/*.cs minimisation.cs integrator.cs ann.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.exe *.dll *.svg *.txt

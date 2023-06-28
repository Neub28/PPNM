
.PHONY : all
all : Linpol.svg Qspline.svg Out.txt
open : 
	xdg-open Linpol.svg
	xdg-open Qspline.svg

Qspline.svg : qspline.gpi qtabvalues.txt qsplinevalues.txt
	gnuplot $<

Linpol.svg : linpol.gpi tabvalues.txt intpolvalues.txt pointvalues.txt integralvalues.txt
	gnuplot $<

Out.txt tabvalues.txt intpolvalues.txt pointvalues.txt qtabvalues.txt qsplinevalues.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/*.cs linearinterpol.cs qspline.cs
	mcs -target:library -out:$@ $^

.PHONY: clean
clean : 
	rm -f *.dll *.txt *.exe *.svg

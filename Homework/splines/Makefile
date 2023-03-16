
.PHONY : all
all : linpol.svg Out.txt

linpol.svg : linpol.gpi tabvalues.txt intpolvalues.txt pointvalues.txt integralvalues.txt
	gnuplot $<

Out.txt tabvalues.txt intpolvalues.txt pointvalues.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/*.cs linearinterpol.cs
	mcs -target:library -out:$@ $^

.PHONY: clean
clean : 
	rm -f *.dll *.txt *.exe *.svg

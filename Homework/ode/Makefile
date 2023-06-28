
.PHONY: 
all : PartA.svg Lotka.svg Out.txt

PartA.svg : parta.gpi diffeq1.txt diffeq2.txt
	gnuplot $<

Lotka.svg : lotka.gpi diffeq3.txt
	gnuplot $<

diffeq.svg : diffeq.gpi diffeq1.txt diffeq2.txt
	gnuplot $<

Out.txt diffeq1.txt diffeq2.txt diffeq3.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/[!ode]*.cs ode.cs
	mcs -target:library -out:$@ $^

.PHONY : clean
clean : 
	rm -f *.dll *.exe *.svg *.txt


diffeq.svg : diffeq.gpi diffeq1.txt diffeq2.txt
	gnuplot $<

Out.txt diffeq1.txt diffeq2.txt : main.exe
	mono main.exe

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/*.cs ode.cs
	mcs -target:library -out:$@ $^

.PHONY : clean
clean : 
	rm -f *.dll *.exe *.svg *.txt

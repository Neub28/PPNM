.PHONY : 
all : Partb.svg Plainmc.svg Out.txt

Partb.svg : partb.gpi qintg.txt pintg.txt
	gnuplot $<

Plainmc.svg : plainmc.gpi unitcirc.txt
	gnuplot $<

Out.txt unitcirc.txt qintg.txt pintg.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs mcint.cs
	mcs -target:library -out:$@ $^

.PHONY : 
clean : 
	rm -f *.svg *.txt *.exe *.dll *.log

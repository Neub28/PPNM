
plainmc.svg : plainmc.gpi unitcirc.txt
	gnuplot $<

Out.txt unitcirc.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs mcint.cs
	mcs -target:library -out:$@ $^

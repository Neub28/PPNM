

Out.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : ../classes/*.cs minimisation.cs
	mcs -target:library -out:$@ $^

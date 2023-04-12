
Out.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : integrator.cs ../classes/*.cs
	mcs -target:library -out:$@ $^


Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/vector.cs ../classes/matrix.cs jacobi_diag.cs
	mcs -target:library -out:$@ $^

.PHONY : clean
clean : 
	rm *.dll *.exe *.txt

Out.txt : main.exe
	mono main.exe > Out.txt

vec.dll : vec.cs
	mcs vec.cs -target:library -out:vec.dll vec.cs

main.exe : main.cs vec.dll
	mcs main.cs -reference:vec.dll -out:main.exe

.PHONY : 
clean : 
	rm -f *.txt *.exe *.dll

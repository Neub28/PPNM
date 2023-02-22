Out.txt : main.exe
	mono main.exe > Out.txt < input.txt

main.exe : main.cs genlist.dll
	mcs main.cs -reference:genlist.dll -out:main.exe

genlist.dll : genlist.cs
	mcs genlist.cs -target:library -out:genlist.dll genlist.cs

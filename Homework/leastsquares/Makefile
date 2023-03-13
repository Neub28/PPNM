
Out.txt : main.exe data.txt
	mono < data.txt main.exe > Out.txt

data.txt : make_data.exe
	mono make_data.exe \
		-t:1,2,3,4,6,9,10,13,15 \
		-y:117,100,88,72,53,29.5,25.2,15.2,11.1 \
		-dy:5,5,5,4,4,3,3,2,2

make_data.exe : make_data.cs
	mcs make_data.cs

main.exe : main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll -out:main.exe

matrix.dll : ../classes/vector.cs ../classes/matrix.cs ../classes/QRGS.cs leastsquares.cs
	mcs -target:library -out:$@ $^

.PHONY: clean
clean : 
	rm -f *.dll *.txt *.exe *.eps

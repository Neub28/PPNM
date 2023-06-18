
Out.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs integration.dll
	mcs main.cs -reference:integration.dll

integration.dll : cubature.cs 
	mcs cubature.cs -target:library -out:$@

.PHONY : 
clean : 
	rm -f *.txt *.exe *.dll


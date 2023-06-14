Out.txt : machineepsilon.exe
	mono machineepsilon.exe > Out.txt

machineepsilon.exe : machineepsilon.cs
	mcs machineepsilon.cs
clean: 
	rm -f Out.txt machineepsilon.exe

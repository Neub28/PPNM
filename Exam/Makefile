TIME = time --portability --append --output $@

Out.txt : main.exe pcubas.py
	$(TIME) mono $< >> $@
	$(TIME) python3 pcubas.py >> $@

main.exe : main.cs integration.dll
	mcs main.cs -reference:integration.dll

integration.dll : cubature.cs 
	mcs cubature.cs -target:library -out:$@

.PHONY : 
clean : 
	rm -f *.txt *.exe *.dll


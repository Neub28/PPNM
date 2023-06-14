TIME = time --portability --append --output $@

N = 2e8
Out.txt : main.exe pfor.exe
	>$@
	$(TIME) mono main.exe -terms:$N -threads:1 >> $@
	$(TIME) mono main.exe -terms:$N -threads:2 >> $@
	$(TIME) mono main.exe -terms:$N -threads:3 >> $@
	$(TIME) mono main.exe -terms:$N -threads:4 >> $@
	@echo Testing Parallel.For >> $@
	$(TIME) mono pfor.exe -terms:$N -threads:1 >> $@

main.exe : main.cs
	mcs $<

pfor.exe : pfor.cs
	mcs $<

N = 5e8
test : out1.txt out2.txt out3.txt out4.txt
out1.txt : main.exe ; mono main.exe -terms:$N -threads:1 >> $@
out2.txt : main.exe ; mono main.exe -terms:$N -threads:1 >> $@  
out3.txt : main.exe ; mono main.exe -terms:$N -threads:1 >> $@
out4.txt : main.exe ; mono main.exe -terms:$N -threads:1 >> $@

.PHONY : 
clean : 
	rm -f *.txt *.exe

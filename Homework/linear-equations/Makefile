TIME = time --portability --append --output $@

.PHONY : make
make: Out.txt Graph.eps main.exe data_times.txt 

Out.txt : main.exe 
	mono main.exe > Out.txt

Graph.eps : graph.gpi data_times.txt
	gnuplot $<
	echo "NB: The plot of QR-decomp. of different sizes n er in file Graph.eps"

main.exe: main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll -out:main.exe

data_times.txt : times_table.txt data_picker.exe
	mono data_picker.exe < $^ > $@

data_picker.exe : data_picker.cs
	mcs data_picker.cs

QR_times.exe : QR_times.cs matrix.dll
	mcs QR_times.cs -reference:matrix.dll -out:QR_times.exe

times_table.txt : QR_times.exe 
	echo n 10 > $@
	$(TIME) mono $< -n:10 >>$@
	echo n 50 >> $@
	$(TIME) mono $< -n:50 >>$@
	echo n 200 >> $@
	$(TIME) mono $< -n:200 >>$@
	echo n 400 >> $@
	$(TIME) mono $< -n:400 >>$@
	echo n 600 >> $@
	$(TIME) mono $< -n:600 >>$@

matrix.dll : ../classes/matrix.cs ../classes/vector.cs QRGS.cs 
	mcs -target:library -out:$@ $^

.PHONY: clean
clean: 
	rm -f *.dll *.txt *.exe *.eps *.log

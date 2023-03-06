TIME = time --portability --append --output $@

Out.txt : main.exe 
	mono main.exe > Out.txt

main.exe: main.cs matrix.dll 
	mcs main.cs -reference:matrix.dll -out:main.exe

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

matrix.dll : matrix.cs vector.cs QRGS.cs 
	mcs -target:library -out:$@ $^

clean: 
	rm -f *.dll *.txt *.exe *.svg

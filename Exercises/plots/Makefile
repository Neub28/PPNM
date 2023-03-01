plots : Gamma.svg Error_func.svg Lngamma.svg

Gamma.svg : gamma.gpi gamma.data factorials.data
	gnuplot $<

Error_func.svg : error_func.gpi error_func.data tab_erf.data
	gnuplot $<

Lngamma.svg : lngamma.gpi lngamma.data tab_lngamma.data
	gnuplot $<

error_func.data : main.exe
	mono main.exe -erf > $@

gamma.data : main.exe
	mono main.exe -gamma > $@

lngamma.data : main.exe
	mono main.exe -lngamma > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -reference:,$(filter %.dll,$^))

sfuns.dll : gammafunc.cs erf.cs lngamma.cs
	mcs -target:library -out:$@ $^

factorials.data : Makefile
	>$@
	echo 0 1 >>$@
	echo 1 1 >>$@
	echo 2 2 >>$@
	echo 3 6 >>$@
	echo 4 24 >>$@
	echo 5 120 >>$@

tab_erf.data : Makefile
	>$@
	echo 0 0 >>$@
	echo 0.02 0.022564575 >>$@
	echo 1 0.842700793 >>$@
	echo 2 0.995322265 >>$@
	echo 3 0.999977910 >>$@
	echo 3.5 0.999999257 >>$@

tab_lngamma.data : Makefile
	>$@
	echo 0.0078125 4.84757 >>$@
	echo 1 0 >>$@
	echo 2 0 >>$@
	echo 3 0.693147 >>$@
	echo 10 12.801827 >>$@
	echo 11 15.104413 >>$@
	echo 12 17.502308 >>$@

.PHONY : clean
clean : 
	rm *.dll *.exe *.data *.svg

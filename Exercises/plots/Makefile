plots : Gamma.svg Error_func.svg

Gamma.svg : gamma.gpi gamma.data factorials.data
	gnuplot $<

Error_func.svg : error_func.gpi error_func.data tab_erf.data
	gnuplot $<

error_func.data : main.exe
	mono main.exe -erf > $@

gamma.data : main.exe
	mono main.exe -gamma > $@

main.exe : main.cs sfuns.dll
	mcs -target:exe -out:$@ \
		$(filter %.cs, $^) $(addprefix -reference:,$(filter %.dll,$^))

sfuns.dll : gammafunc.cs erf.cs
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
	echo 0.04 0.045111106 >>$@
	echo 0.06 0.067621594 >>$@
	echo 0.08 0.090078126 >>$@
	echo 0.1 0.112462916 >>$@

.PHONY : clean
clean : 
	rm *.dll *.exe *.data *.svg

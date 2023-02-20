Out.txt : main.exe
	mono $< 1> $@ 2> log

main.exe : main.cs genlist.dll
	mcs -target:exe -out:$@ \
		$(addprefix -reference:,$(filter %.dll, $^)) \
		$(filter %.cs,$^)

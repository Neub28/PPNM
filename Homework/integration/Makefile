.PHONY : all
all : erf.svg Out.txt

erf.svg : erf.gpi erf.txt
	gnuplot $<

Out.txt erf.txt : main.exe 
	mono main.exe > Out.txt

main.exe : main.cs matrix.dll
	mcs main.cs -reference:matrix.dll

matrix.dll : integrator.cs ../classes/*.cs
	mcs -target:library -out:$@ $^

.PHONY : pyint
pyint : pythoncc.py
	python3 pythoncc.py

.PHONY : clean
clean : 
	rm -f *.txt *.exe *.svg

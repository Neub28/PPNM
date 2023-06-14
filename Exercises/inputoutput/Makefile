t1 := ./Task1
t2 := ./Task2
t3 := ./Task3
paths = $(t1) $(t2) $(t3)

.PHONY : 
all : Out1.txt Out2.txt Out3.txt

cat : 
	cat Out1.txt 
	@echo --------End 1-----------
	cat Out2.txt
	@echo --------End 2-----------
	cat Out3.txt
	@echo --------End 3-----------
Out1.txt : 
	mcs $(t1)/main.cs -out:$(t1)/main.exe
	mono $(t1)/main.exe -numbers:1,2,3,4,5 > Out1.txt

Out2.txt : 
	mcs $(t2)/main.cs -out:$(t2)/main.exe
	echo 1 2 3 4 5 | mono $(t2)/main.exe > Out2.txt

Out3.txt : 
	mcs $(t3)/main.cs -out:$(t3)/main.exe
	mono $(t3)/main.exe -input:$(t3)/input.txt -output:Out3.txt

clean : 
	rm -f *.txt

cleanPaths : 
	@$(foreach path,$(paths), \
		cd $(path); \
		rm -f *.exe Out.txt; \
		cd ..; \
	)


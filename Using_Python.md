Python input can be made flexible with
```
import fileinput
for line in fileinput.input():
    process(line)
```
check out [fileinput](https://docs.python.org/3/library/fileinput.html)
This way you can keep your file flexible to manage input from files
```
./myfile.py file.test
```
or from standard input
```
echo "this is input" | ./myfile.py
```

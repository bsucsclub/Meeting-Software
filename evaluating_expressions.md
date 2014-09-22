Hey,
  I was thinking about the practice problem with evaluating in C#.
  I got this far
```
public static String eval(string x, string y,string op){
    if (op =="+"){
        return (int.Parse(x)+int.Parse(y)).ToString();
    }
    else
        return (int.Parse(x)-int.Parse(y)).ToString();
}
Console.WriteLine(eval(eval("1","2","+"),"4","-"));
```
This return -1
But from here the recursion needed to figure it out blew my mind.
I think we could str.split("=")
then use every expression in every location and and compare the two.
I'll keep working on it later.

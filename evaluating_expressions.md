Hey,
  I was thinking about the practice problem with evaluating in C#.
  I got this far
  
```
using System;
public class Test
{
	public static void Main()
	{
		var first = "523=10";
		var second="++";
		var one = first.Split('=')[0];
		var two = first.Split('=')[1];
	   	Console.WriteLine(eval(eval("5","2",'+'),"3",'+').Equals(two));
	}
	public static	String eval(string x, string y, char op){
    if (op =='+')
        return (int.Parse(x)+int.Parse(y)).ToString();
    else
        return (int.Parse(x)-int.Parse(y)).ToString();
	}
}
```

For another day

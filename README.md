Work in progress. A C# take on https://github.com/john-prutton/dep-watcher.”


TIL : Records behave like strings in C#, they are reference types like strings, the ToString method is overloaded for you, so you can console.WriteLine(recordName) and you should expect to see the values on the console.
TIL: Records are immutable, so once created, you will not be able to do something like recordName.X = 5, The (==) is overloaded to check for value comparison, so recond r1 == record r2 if their values are equal which is very cool
TIL: Since they are reference types, Record r1 = r2 will result in them pointing to the same address in memory.
TIL: you can use the with keyword
TIL: you can do a positional record like public record PointR (int X, int Y, int Z) and you can insantiate a new PointR as follows: PointR r = new PointR(1,2,4), you have to use the "(" and not the curly braces

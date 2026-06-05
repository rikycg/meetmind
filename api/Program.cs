Console.WriteLine("Hello, World!");

Console.WriteLine("--------------- Args ---------------");

PrintArgs(args);

void PrintArgs(string[] args)
{
    foreach (var arg in args)
    {
        Console.WriteLine(arg);
    }
}
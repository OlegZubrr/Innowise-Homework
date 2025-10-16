namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        for (int i = 0; i < 1000; i++)
        {
            var res = isTwo(i);
            Console.WriteLine($"{i} {res}");
        }
    }

    static bool isTwo(int n)
    {
        if (n < 2) return false;
        return (n & (n-1)) == 0;
     
    }
}
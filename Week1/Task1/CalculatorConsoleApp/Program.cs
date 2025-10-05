using System.Globalization;

namespace CalculatorConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        
        bool isProcesing = true;

        while (isProcesing)
        {
            double a;
            double b;
            char operation;
            char[] operations = {'+', '-', '*', '/'};
            double result;
       
        
            Console.Write("Enter the first real number: ");
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out a))
            {
                Console.WriteLine("Incorrect input");
                Console.Write("Enter the first real number: ");
            }

            Console.Write("Enter operation (+, -, *, /): ");
            while (!char.TryParse(Console.ReadLine(), out operation) || !operations.Contains(operation))
            {
                Console.WriteLine("Incorrect input");
                Console.Write("Enter operation (+, -, *, /): ");
            }
        
            Console.Write("Enter the second real number: ");
            while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out b))
            {
                Console.WriteLine("Incorrect input");
                Console.Write("Enter the second real number: ");
            }
        
            result = Calculate(a,b,operation);
            if (double.IsNaN(result) || double.IsInfinity(result))
            {
                Console.WriteLine("You can't divide by zero");
                continue;
            }
            Console.WriteLine($"{a} {operation} {b} = {Math.Round(result,3)}");

            Console.Write("Continue ? [Y/n]: ");
            
            string? answer = Console.ReadLine();
            while (!string.IsNullOrEmpty(answer) && answer.ToLower() != "y" && answer.ToLower() != "n")
            {
                Console.WriteLine("Incorrect input");
                Console.Write("Continue ? [Y/n]: ");
                answer = Console.ReadLine();
            }
            
            isProcesing = answer.ToLower() == "y"  || string.IsNullOrEmpty(answer);
            
        }
      
    }

    static double Calculate(double a, double b,char operation)
    {
        double result = 0;

        switch (operation)
        {
            case '+':
                result = a + b;
                break;
            case '-':
                result = a - b;
                break;
            case '*':
                result = a * b;
                break;
            case '/':
                if (b == 0)
                {
                    result =  double.NaN; 
                    return result;
                }
                result =  a / b;
                break;

        }
        return result;
    }
}

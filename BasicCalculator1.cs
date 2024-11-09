using System;

class Program
{
    static void Main()
    {
        int option = -1;
        float number1, number2, result = 0;

        while (option != 0)
        {
            Console.WriteLine("*********** Basic Calculator for Two Numbers ***********");
            Console.WriteLine("\t1. Add");
            Console.WriteLine("\t2. Subtract");
            Console.WriteLine("\t3. Multiply");
            Console.WriteLine("\t4. Divide");
            Console.WriteLine("\n\t0. Exit");

            Console.Write("\n\tChoose an option: ");
            try
            {
                option = int.Parse(Console.ReadLine()!);

                if (option == 1 || option == 2 || option == 3 || option == 4)
                {
                    Console.Write("Enter the first number: ");
                    number1 = float.Parse(Console.ReadLine()!);

                    Console.Write("Enter the second number: ");
                    number2 = float.Parse(Console.ReadLine()!);

                    switch (option)
                    {
                        case 1:
                            result = number1 + number2;
                            Console.WriteLine("Selected option: Addition");
                            break;

                        case 2:
                            result = number1 - number2;
                            Console.WriteLine("Selected option: Subtraction");
                            break;

                        case 3:
                            result = number1 * number2;
                            Console.WriteLine("Selected option: Multiplication");
                            break;

                        case 4:
                            if (number2 != 0)
                                result = number1 / number2;
                            Console.WriteLine("Selected option: Division");
                            break;
                    }

                    if (option == 4 && number2 == 0)
                        Console.WriteLine("Cannot divide by zero!");
                    else
                        Console.WriteLine($"The result is: {result} \n\n");
                }
                else if (option != 0)
                {
                    Console.WriteLine("Invalid option. Try again!\n");
                }
            }
            catch (FormatException capturedError)
            {
                Console.WriteLine("Non-numeric input. Try again!");
                Console.WriteLine(capturedError.Message + "\n");
            }
        }

        Console.WriteLine("Execution Completed. You have chosen to exit.");
    }
}

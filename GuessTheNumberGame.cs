using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Guess the Random Number Game (1 to 100)");

        /*
         1. Generate a random number between 1 and 100 - Check!
         2. Create an attempt counter - Check!
         3. Compare if the guess is too high, too low, or correct
         4. If the number is found, end the execution and display the attempt count.
        */

        Random random = new();
        int targetNumber = random.Next(1, 101); // upper limit is exclusive
        //Console.WriteLine("\nThe target number is {0}", targetNumber);

        int attemptCounter = 0, guessedNumber = 0;
        bool isValidInput;

        // repeat the loop until the guessed number matches the target number
        while (targetNumber != guessedNumber)
        {
            isValidInput = false; // reset control variable for reading input
            while (!isValidInput)
            {
                try
                {
                    Console.Write("\nWhat do you think the target number is? ");
                    guessedNumber = int.Parse(Console.ReadLine()!);

                    isValidInput = true;

                    if (guessedNumber >= 1 && guessedNumber <= 100)
                    {
                        attemptCounter++;

                        // Identify if the guess is too high, too low, or correct
                        if (guessedNumber > targetNumber)
                            Console.WriteLine("Too high! The target number is lower!");

                        if (guessedNumber < targetNumber)
                            Console.WriteLine("Too low! The target number is higher!");

                        if (guessedNumber == targetNumber)
                            Console.WriteLine("\nYou won an imaginary cupcake! You found the number!");
                    }
                    else
                    {
                        Console.WriteLine("The entered number is out of range. Try again!");
                    }
                }
                catch (FormatException error)
                {
                    Console.WriteLine("That's not a valid number. Try again!");
                    Console.WriteLine(error.Message + "\n");
                }
            }
        }

        Console.WriteLine($"It took you {attemptCounter} attempts to find the number.");
    }
}

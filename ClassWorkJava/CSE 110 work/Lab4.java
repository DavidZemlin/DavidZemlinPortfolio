/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab4.java
// SPECIFICATION: Gives the user a choice of 3 different arithmetic operation to compute: sum of all integers from 1 to a number, the factorial of a number, or the leftmost digit of a number.
// FOR: CSE 110- Lab #4
// TIME SPENT: 2 hours
//-----------------------------------------------------------*/

import java.util.Scanner;

public class Lab4 
{
	public static void main(String[] args)
	{
		// Variables
		Scanner scan = new Scanner(System.in);
		int choise = 0;
		int number;
		
		// Open Menu
		do
		{
			// Lab4.pdf shows a space before the first line of output and each reprint of the menu, so insert the space
			System.out.println();
			
			// print menu
			displayMenu();
			choise = scan.nextInt();
			int result;
			
			// start the operation the user selected
			switch (choise)
			{
			
				// prompt user for a number and run summation method
				case 1:
					System.out.println("Enter a number:");
					number = scan.nextInt();
					result = sumOfDigitsFrom1To(number);
					System.out.println("The sum of 1 to " + number + " is " + result);
					break;
					
				// prompt user for a number and run factorial method
				case 2:
					System.out.println("Enter a number:");
					number = scan.nextInt();
					result = factorialOf(number);
					switch (result)
					{
						case -1:
							System.out.println("ERROR- ya dun goofed. positive numbers only please.");

							break;
						default:
							System.out.println("The factorial of " + number + " is " + result);
							break;
					}
					break;
					
				// prompt user for a number and run left digit finder method
				case 3:
					System.out.println("Enter a number:");
					number = scan.nextInt();
					result = leftMostDigit(number);
					System.out.println("The leftmost digit of " + number + " is " + result);
					break;
					
				// quit program
				case 4:
					System.out.print("Bye");
					break;
			}
		}
		while (choise != 4);
		
		//Close input scanner to prevent resource leaks.
		scan.close();
	}
	
	/**
	 * Print the menu
	 */
	public static void displayMenu()
	{
        System.out.println("Please choose one option from the following menu:");
        System.out.println("1) Calculate the sum of integers from 1 to m");
        System.out.println("2) Calculate the factorial of a given number");
        System.out.println("3) Display the leftmost digit of a given number");
        System.out.println("4) Quit");
	}
	
	/**
	 * calculates the total of all integers from 1 to a given number
	 * @param number the last number to be summed up
	 * @return the resulting sum
	 */
	public static int sumOfDigitsFrom1To(int number)
	{
		//run through each integer from 1 to the target number and add them together
		int sum = 0;
		for (int i = 1; i <= number; i++)
		{
			sum += i;
		}
		return sum;
	}
	
	/**
	 * finds the factorial of a given positive number
	 * @param number number to be used
	 * @return the factorial of the provided number. returns -1 if invalid (negative) number is provided
	 */
	public static int factorialOf(int number)
	{
		int factorial = 0;
		
		// send error flag if a negative number in entered
		if (number < 0)
		{
			factorial = -1;
		}
		
		// if 0 or 1 is entered then the factorial is just 1
		else if (number < 2)
		{
			factorial = 1;
		}
		
		// run calculation for any other number
		else
		{
			factorial = number;
			for (int i = number - 1; i > 0; i--)
			{
				factorial *= i;
			}
		}
		return factorial;
	}
	
	public static int leftMostDigit(int number)
	{
		int result = number;
		
		// check is a negative number was submitted and get the absolute value if it is
		if (number < 0)
		{
			result = Math.abs(result);
		}
		
		// if the number is 10 or higher, divide by 10 until only the leftmost digit remains
		while (result > 9)
		{
			result /= 10;
		}
		return result;
	}
}

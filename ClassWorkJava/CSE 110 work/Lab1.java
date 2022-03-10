/*------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab1.java
// SPECIFICATION: A program to return the average of 3 test scores.
// FOR: CSE 110- Lab #1
// TIME SPENT: 45 minutes
//-----------------------------------------------------------*/
import java.util.Scanner;

public class Lab1 
{
	public static void main(String[] args)
	{
		int test1, test2, test3;
		final int NUMBER_OF_TESTS = 3;
		double averageScore;
		
		//Prompt user for 3 test grades and record them.
		Scanner input = new Scanner(System.in);
		System.out.println("Enter the score on the first test: ");
		test1 = input.nextInt();
		System.out.println("Enter the score on the second test: ");
		test2 = input.nextInt();
		System.out.println("Enter the score on the third test: ");
		test3 = input.nextInt();
		
		//Add all the test grades together and divide the result by the number of tests.
		averageScore = (test1 + test2 + test3) / (double) NUMBER_OF_TESTS;
		
		//Display the calculated average to the user.
		System.out.println("Your average score is: " + averageScore);
		
		//Close input to prevent resource leaks.
		input.close();
	}
}

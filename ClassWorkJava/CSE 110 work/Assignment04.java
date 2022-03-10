// CSE 110     : CES 110 / Online
// Assignment  : 04
// Author      : David Zemlin 1223802827
// Description : A number guessing game

import java.util.Scanner;

public class Assignment04 
{
    public static void main(String[] args) 
    {
    	// constants
    	final int NUMBER_OF_DIGITS = 4;
    	
    	// variables
    	Scanner scan = new Scanner(System.in);
    	int[] secretNumber = new int[NUMBER_OF_DIGITS];
    	boolean play = true;
    	
    	// while the user wants to play another game
    	do
    	{
    		// print game tile and message
    		System.out.println("----- MASTERMIND -----");
    		System.out.println("Guess the " + secretNumber.length + " digit number!");
    		
    		// pick a new secret number
    		for (int i = 0; i < secretNumber.length; i++)
    		{
    			secretNumber[i] = (int) (Math.random() * 10);
    		}
    		
    		// loop variables
    		int guess = 0;
    		int guessCount = 1;
			int matchedDigitCount = 0;
    		
    		// while the user's guess is not completely correct
    		do
    		{
    			// reset matched digit count
    			matchedDigitCount = 0;
    			
	    	 	// prompt the user to make a guess and record it
    			System.out.println();
    			System.out.print("Guess " + guessCount + ": ");
    			guess = scan.nextInt();
    			
    			// check if user entered too large a number
    			if (guess >= (int) (Math.pow(10,  NUMBER_OF_DIGITS)))
    			{
    				System.out.println("Invald input. Number too large.");
    			}
    			else
    			{
	    			// break user'a guess into an array
	    			int[] guessDigits = new int[secretNumber.length];
	    			int digitCounter = 0;
	    			for (int i = secretNumber.length; i > 0; i--)
	    			{
	    				// cut of digits to the left
	    				guessDigits[digitCounter] = (int) (guess % Math.pow(10, i));
	    				
	    				// cut off digits to the right
	    				if (i != 1)
	    				{
	    					guessDigits[digitCounter] = (int) (guessDigits[digitCounter] / Math.pow(10, i - 1));
	    				}
	    				digitCounter++;
	    			}
	    			
	    			// find out how many numbers are correct
	    			for (int i = 0; i < secretNumber.length; i++)
	    			{
	    				if (guessDigits[i] == secretNumber[i])
	    				{
	    					matchedDigitCount++;
	    				}
	    			}
	    			
		    	 	// display number of correct digits in user's guess
	    			System.out.println("You matched " + matchedDigitCount);
	    			
	    			// increment guess counter
	    			guessCount++;
    			}
    		}
    		while(matchedDigitCount != secretNumber.length);
    		
		 	// congratulate the user and tell them the number of guesses they took
			System.out.println();
    		System.out.println("Congratulations! You guessed the right number in " + (guessCount - 1) + " guesses.");
    		
		 	// ask the user if they want to play again
    		String playAgain;
    		do
    		{
    			System.out.print("Would you like to play again (yes/no)?");
    			playAgain = scan.next();
    			if (!playAgain.equals("yes") && !playAgain.equals("no"))
    			{
    				System.out.println("Invald input.");
    			}
    			else if (playAgain.equals("no"))
    			{
    				play = false;
    			}
    		}
    		while (!playAgain.equals("yes") && !playAgain.equals("no"));
    		
    		if (play)
    		{
    			System.out.println();
    		}
    	}
    	while (play);
    	
		// close input scanner to prevent resource leaks.
		scan.close();
    }
}

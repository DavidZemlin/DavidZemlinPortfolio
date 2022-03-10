/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab5.java
// SPECIFICATION: Builds one of 3 different text images customized by user input
// FOR: CSE 110- Lab #5
// TIME SPENT: 3 hours
//-----------------------------------------------------------*/

import java.util.Scanner;

public class Lab5 
{
	public static void main(String[] args)
	{
		// constants 
        final char SIDE_SYMB = '-';
        final char MID_SYMB = '*';
        final char DIAMOND_OUTSIDE = '*';
        final char DIAMOND_FILL = ' ';
        
        // variables
        Scanner scanner = new Scanner(System.in);
        String inputStr = "";
        char choice = ' ';
        int numSymbols = -1;
        int sideWidth = -1;
        int midWidth = -1;
        
        // open menu
        do
        {
        	displayMenu();
        	inputStr = scanner.nextLine();
        	if (inputStr.length() > 0)
        	{
        		choice = inputStr.charAt(0);
        	}
        	else
        	{
        		choice = ' ';
        	}
        	
        	// determine user selection and display follow up questions
        	switch (choice)
        	{
        		// row chosen
        		case 'r':
        			System.out.println("Width of the sides?");
                    sideWidth = scanner.nextInt();
                    System.out.println("Width of the middle?");
                    midWidth = scanner.nextInt();
                    System.out.println();
                    buildRow(SIDE_SYMB, sideWidth, MID_SYMB, midWidth);
                	scanner.nextLine();  // Flush junk newline symbols
        			break;
        			
        		// Pyramid chosen
        		case 'p':
                    System.out.println("Number of symbols on the lowest layer?");
                    numSymbols = scanner.nextInt(); 
                    System.out.println();
                    reportIfEven(numSymbols);
                    buildPyramid(SIDE_SYMB, MID_SYMB, numSymbols);
                	scanner.nextLine();  // Flush junk newline symbols
        			break;
        			
        		// diamond chosen	
        		case 'd':
                    System.out.println("Number of symbols on the middle layer?");
                    numSymbols = scanner.nextInt(); 
                    System.out.println();
                    reportIfEven(numSymbols);
                    buildDiamond(DIAMOND_OUTSIDE, DIAMOND_FILL, numSymbols);
                	scanner.nextLine();  // Flush junk newline symbols
        			break;
        		
        		// quit program chosen
        		case 'q':
        			System.out.println("Bye");
        			break;
        		
    			// invalid input from user
                default:
                    System.out.println("Please choose a valid option from the menu.");
                    break;
        	}        	
        }
        while (choice != 'q');
        
        scanner.close();
	}
	
	/**
	 * send a message if even number was given when an odd was expected
	 * @param numSymbols number to be checked
	 */
	private static void reportIfEven(int numSymbols)
	{
        if (numSymbols % 2 == 0)
        {
        	System.out.println("The input is not an odd number. Use the closest odd number below " + numSymbols);
        }
	}
	/**
	 * Creates a string of one character repeated a given number of times
	 * @param character character to be repeated
	 * @param length number of times character is repeated
	 * @return string of repeated characters
	 */
	private static String oneCharacterLine(char character, int length)
	{
		String result = "";
		for (int i = 0; i < length; i++)
		{
			result = result + character;
		}
		return result;
	}
	
    /**
     * Build a row of symbols (pattern) with the given parameters. 
     *
     * For example, -----*****----- can be built by the parameters
     *
     * sideWidth = 5, midWidth = 5, sideSymb = '-', midSymb = '*'
     * 
     * @param sideSymb  A char to be repeated on both sides
     * @param sideWidth Number of symbols on each side
     * @param midSymb   A char to be repeated in the middle
     * @param midWidth  Number of symbols in the middle
     */
	private static void buildRow(char sideSymb, int sideWidth, char midSymb, int midWidth)
	{
		System.out.print(oneCharacterLine(sideSymb, sideWidth));
		System.out.print(oneCharacterLine(midSymb, midWidth));
		System.out.println(oneCharacterLine(sideSymb, sideWidth));
	}
	
    /**
     * Build a pyramid pattern with the given parameters.
     *
     * can be built by sideSymb = '-', midSymb = '*', numSymbols = 11
     *
     * When ptnHeight is not an odd integer, replace it by the closest
     * even integer below. For example, if numSymbols is 10, use 9 instead.
     * 
     * When ptnHeight is 0, return an empty String.
     * 
     * @param  sideSymb   A char to be repeated on both sides
     * @param  midSymb    A char to be repeated in the middle
     * @param  numSymbols The number of symbols on the lowest layer; a negative number yields an upside down pyramid minus its top layer
     */
	private static void buildPyramid(char sideSymb, char midSymb, int numSymbols)
	{
		// check is number is negative, if so, mark it as inverted and get absolute value
		int charPerRow = numSymbols;
		boolean inverted = false;
		if (charPerRow < 0)
		{
			inverted = true;
			charPerRow = Math.abs(charPerRow);
		}
		
		// determine how many rows will be made
		int rowCount;
		if (charPerRow % 2 == 0)
		{
			rowCount = charPerRow / 2;
		}
		else
		{
			rowCount = (charPerRow + 1) / 2;
		}
		
		// make a pyramid, but check if it should be upside down
		if (inverted)
		{
			// build upside down
			for (int i = rowCount - 2; i >= 0; i--)
			{
				int mids = 1 + (2 * i);
				int sides = (charPerRow - mids) / 2;
				buildRow(sideSymb, sides, midSymb, mids);
			}
		}
		else
		{
			// build right side up
			for (int i = 0; i < rowCount; i++)
			{
				int mids = 1 + (2 * i);
				int sides = (charPerRow - mids) / 2;
				buildRow(sideSymb, sides, midSymb, mids);
			}
		}
	}
	
	/**
     * Build a diamond pattern. The parameters are the same 
     * as {@link #buildPyramid(char, char, int)}.
     * 
     * @param  sideSymb  A char to be repeated on both sides
     * @param  midSymb   A char to be repeated in the middle
     * @param  numSymbols The width of the pattern
	 */
	private static void buildDiamond(char sideSymb, char midSymb, int numSymbols)
	{
		buildPyramid(sideSymb, midSymb, numSymbols);
		buildPyramid(sideSymb, midSymb, numSymbols * -1);
	}
	
	/**
	 * Display the menu
	 */
	private static void displayMenu()
	{
		System.out.println();
        System.out.println("Please choose one pattern from the list:");
        System.out.println("r) Row");
        System.out.println("p) Pyramid");
        System.out.println("d) Shallow diamond");
        System.out.println("q) Quit");
	}
}

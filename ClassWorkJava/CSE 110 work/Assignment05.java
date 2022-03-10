// CSE 110     : CES 110 / Online
// Assignment  : 05
// Author      : David Zemlin 1223802827
// Description : A collection of ten methods with various purposes

public class Assignment05
{

    public static void main(String[] args)
    {
        // test cases
        displayGreeting();
        displayText("test");
        printTotal(1,2,3);
        int total = getTotal(1,2,3);
        System.out.println(total);
        double average= getAverage(1,2,3);
        System.out.println(average);
        average= averageLength("test","test21", "testtest");
        System.out.println(average);
        int shortest = lengthOfShortest("test","test21");
        System.out.println(shortest);
        String stars=stringOfStars("test");
        System.out.println(stars);
        stars=maxStringOfStars("test","test21");
        System.out.println(stars);
        stars=midStringOfStars("test","test21", "testtest");
        System.out.println(stars);

    }
    
    // prints the text "Hello, and welcome!".
    static void displayGreeting()
    {
    	System.out.println("Hello, and welcome!");
    }
    
    // prints the value of the argument that was passed to it.
    static void displayText(String text)
    {
    	System.out.println(text);
    }
    
    // prints the sum of the three arguments passed to it. 
    static void printTotal(int number1, int number2, int number3)
    {
    	System.out.println(number1 + number2 + number3);
    }
    
    // returns the sum as an int of the three arguments passed to it.
    static int getTotal(int number1, int number2, int number3)
    {
    	return number1 + number2 + number3;
    }
    
    // returns a double that equals the average of the three arguments passed to it.
    static double getAverage(int number1, int number2, int number3)
    {
    	return (double) (number1 + number2 + number3) / 3.0;
    }
    
    // returns a double that equals the average number of characters of the strings passed to it.
    static double averageLength(String string1, String string2, String string3)
    {
    	return (double) (string1.length() + string2.length() + string3.length()) / 3.0;
    }
    
    // returns the number of characters of the shortest string argument passed to it.
    static int lengthOfShortest(String string1, String string2)
    {
    	int shortestLength = string1.length();
    	if (string1.length() > string2.length())
    	{
    		shortestLength = string2.length();
    	}
    	return shortestLength;
    }
    
    // returns a string of asterisks of the same length as the string passed to it.
    static String stringOfStars(String string)
    {
    	String starString = "";
    	for (int i = 0; i < string.length(); i++)
    	{
    		starString = starString + "*";
    	}
    	return starString;
    }
    
    // returns a string of asterisks of the same length as the longest string passed to it.
    static String maxStringOfStars(String string1, String string2)
    {
    	int longestLength = string1.length();
    	if (string1.length() < string2.length())
    	{
    		longestLength = string2.length();
    	}
    	
    	String starString = "";
    	for (int i = 0; i < longestLength; i++)
    	{
    		starString = starString + "*";
    	}
    	return starString;
    }
    
    // returns a string of asterisks of the same length as the string with the middle length of three strings passed to it.
    static String midStringOfStars(String string1, String string2, String string3)
    {
    	int[] lengths = {string1.length(), string2.length(), string3.length()} ;
    	int shortestLength = lengths[0];
    	int longestLength = lengths[0];
    	int midLength = lengths[0]; // start adding lengths into midLength variable
    	
    	for (int i = 1; i < lengths.length; i++)
    	{
    		if (lengths[i] < shortestLength)
    		{
    			shortestLength =  lengths[i];
    		}
    		
    		if (lengths[i] > longestLength)
    		{
    			longestLength =  lengths[i];
    		}
    		
    		midLength = midLength + lengths[i]; // add the others into midLength variable
    	}
    	
    	// determine the true midlength by subtracting the other 2 lengths out of the total
    	midLength = midLength - shortestLength - longestLength;
    	
    	String starString = "";
    	for (int i = 0; i < midLength; i++)
    	{
    		starString = starString + "*";
    	}
    	return starString;
    }
}

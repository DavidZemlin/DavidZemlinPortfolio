/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab2.java
// SPECIFICATION: Read a first and last name, join names into a full name, print the name in all caps, print the length of the name and tell us if string comparison methods using "==" and ".equals" work.
// FOR: CSE 110- Lab #2
// TIME SPENT: 1 Hour and 15 Minutes
//-----------------------------------------------------------*/
import java.util.Scanner;

public class Lab2 
{
	public static void main(String[] args)
	{
		//Main Variables
		String firstName = "";
		String lastName = "";
		String fullName = "";
		int nameLength = 0;
		Scanner scan = new Scanner(System.in);
		
		//Variables for string comparison testing
		final String TITLE1 = new String("cse110");
		final String TITLE2 = "cse110";
		
		
		//Ask user to input a first and last name
		System.out.println("Please enter first name: ");
		firstName = scan.nextLine();
		System.out.println("Please enter last name: ");
		lastName = scan.nextLine();
		
		//Join first and last name and store in fullName variable.
		fullName = firstName + " " + lastName;
		
		//Convert fullName to all caps.
		fullName = fullName.toUpperCase();
		
		//Store Length of full name in its variable
		nameLength = fullName.length();
		
		//Display full name
		System.out.println("Full name (in capitals): " + fullName);
		
		//Display the length of the fullName string
		System.out.println("Length of full name: " + nameLength);
		
		//Test a string with "==" and ".equals" and display the result.
		if (TITLE1 == TITLE2)
		{
			System.out.println("Error! String comparison using \"==\" sign works!?");
		}
		else
		{
			System.out.println("String comparison using \"==\" sign does NOT work");
		}
		
		if (TITLE1.equals(TITLE2))
		{
			System.out.println("String comparison using \"equals\" method works");
		}
		else
		{
			System.out.println("Error! - String comparison using \"equals\" method works but the strings given did not match");
		}
		
		//Close input to prevent resource leaks.
		scan.close();
	}
}

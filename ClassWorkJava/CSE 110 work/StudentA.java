/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Student.java
// SPECIFICATION: a custom data type for storing student information
// FOR: CSE 110- Lab #7
// TIME SPENT: 40 minutes
//-----------------------------------------------------------*/

public class StudentA 
{
	private String firstName;
	private String lastName;
	private String fullName;
	private String asuID;
	private double grade = -1.0;
	
	public StudentA(String fName, String lName, String id)
	{
		firstName = fName;
		lastName = lName;
		fullName  = fName + " " + lName;
		asuID = id;
	}
	
	public String getFirstName()
	{
		return firstName;
	}
	
	public String getLastName()
	{
		return lastName;
	}
	
	public String getFullName()
	{
		return fullName;
	}
	
	public String getAsuID()
	{
		return asuID;
	}
	
	public double getGrade()
	{
		return grade;
	}
	
	public String toString()
	{
		return String.format("Student: %s %s, ASUID: %s, Grade: %.2f", firstName, lastName, asuID, grade);
	}
	
	public void setGrade(double newGrade)
	{
		grade = newGrade;
	}
}

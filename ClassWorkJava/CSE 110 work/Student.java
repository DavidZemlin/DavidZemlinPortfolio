/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Student.java
// SPECIFICATION: a class to store student data
// FOR: CSE 110- Lab #8
// TIME SPENT: 2 hours
//-----------------------------------------------------------*/

public class Student 
{
	// Instance Variables
	private int numOfUpdates = 0;
	private int numOfAccessed = 0;
	private String fullName;
	private String asuID;
	private double grade = -1.0;
	
	// Constructors
	public Student()
	{
		// empty constructor; creates a student with no data.
	}
	
	public Student(String name, String id, double initGrade)
	{
		fullName  = name;
		asuID = id;
		grade = initGrade;
	}
	
	// Getters
	public String getAsuID()
	{
		numOfAccessed++;
		return asuID;
	}
	
	public String getFullname()
	{
		numOfAccessed++;
		return fullName;
	}
	
	public double getGrade()
	{
		numOfAccessed++;
		return grade;
	}
	
	public int getNumOfUpdates()
	{
		numOfAccessed++;
		return numOfUpdates;
	}
	
	public int getNumOfAccessed()
	{
		numOfAccessed++;
		return numOfAccessed;
	}
	
	// Setters
	public void setGrade(double newGrade)
	{
		numOfUpdates++;
		grade = newGrade;
	}
	
	public void setId(String newId)
	{
		numOfUpdates++;
		asuID = newId;
	}
	
	public void setName(String fullName)
	{
		numOfUpdates++;
		this.fullName = fullName;
	}
	
	// Instance methods
	/**
	 * Compares this student to another, returning true if:
	 * 1- they have the same ID, or
	 * 2- they both have names and the names match.
	 * @param other the student object that this object is being compared to.
	 * @return true if they are equal, false if not equal.
	 */
	public boolean equals(Student other)
	{
		if (asuID == other.getAsuID())
		{
			return true;
		}
		else if (fullName != null && other.getFullname() != null)
		{
			String myLowerCaseName = fullName.toLowerCase();
			String otherLowerCaseName = other.getFullname().toLowerCase();
			
			if (myLowerCaseName.equals(otherLowerCaseName))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}
	}
	
	public String toString()
	{
		return String.format("[Name: %s, ASUID: %s, Grade: %.2f]", fullName, asuID, grade);
	}
}

/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab6.java
// SPECIFICATION: Reads student group and grades, outputs student records and group statistics
// FOR: CSE 110- Lab #6
// TIME SPENT: 1 hour
//-----------------------------------------------------------*/

import java.util.Scanner;

public class Lab6
{
	public static void main(String[] args)
	{
		// variables
		Scanner scanner = new Scanner(System.in);
		int numStudents = -1;
		int[] studentGroups = null;
		double[] studentGrades = null;
		int numGroups = -1;
		
		// ask for number of students
		System.out.println("How many students do you have?");
		numStudents = scanner.nextInt() + 1;
		
		// initialize the array based on user input
		studentGroups = new int[numStudents];
		studentGrades = new double[numStudents];
		
		// prompt for and record student data
		for (int i = 1; i < numStudents; i++)
		{
			System.out.println("[Group #] and [Grade] for Entry " + i);
			studentGroups[i] = scanner.nextInt();
			studentGrades[i] = scanner.nextDouble();
			
			// find the number of groups
			if (studentGroups[i] > numGroups)
			{
				numGroups = studentGroups[i];
			}
		}
		
		// print out student records
		System.out.println();
		System.out.println("==== List of Student Records =====");
		for (int i = 1; i < numStudents; i++)
		{
			System.out.println("Group " + studentGroups[i] + " - " + studentGrades[i] + " Points");
		}
		
		// part 2 variables
		double[] groupAverages = new double[numGroups + 1];
		int[] groupSizes = new int[numGroups + 1];
		
		// for each student record
		for (int i = 1; i < numStudents; i++)
		{
			// - find the group # and the grade, and
			int group = studentGroups[i];
			double grade = studentGrades[i];
			
			// - update groupAverages and groupsSizes
			groupAverages[group] += grade;
			groupSizes[group]++;
		}
		
		// average the scores
		for (int i = 1; i < groupAverages.length; i++ )
		{
			groupAverages[i] = groupAverages[i] / groupSizes[i];
		}
		
		// display the statistics of groups
		System.out.println();
		System.out.println("==== Group Statistics ===");
		for (int i = 1; i < groupAverages.length; i++ )
		{
			System.out.printf("Group #%d has %d students, average is %.2f%n", i, groupSizes[i], groupAverages[i]);
		}
		
		//Close input scanner to prevent resource leaks.
		scanner.close();
	}
}
/*-------------------------------------------------------------
// AUTHOR: David Zemlin
// FILENAME: Lab3.java
// SPECIFICATION: A Program that reads the scores of 3 weighted grades and produces an average of them. This program uses input verification.
// FOR: CSE 110- Lab #3
// TIME SPENT: 1 hour and 45 minutes
//-----------------------------------------------------------*/
import java.util.Scanner;

public class Lab3 
{
	public static void main(String[] args)
	{
		// variables
		Scanner scanner = new Scanner(System.in);
		double homeworkGrade = 0;
		double midtermExamGrade = 0;
		double finalExamGrade = 0;
		double weightedTotal = 0;
		int i = 0;
		
		// Constants
		final double HOMEWORK_MAX_RANGE = 100.0;
		final double HOMEWORK_WEIGHT = 0.25;
		final double MIDTERM_EXAM_MAX_RANGE = 100.0;
		final double MIDTERM_EXAM_WEIGHT = 0.25;
		final double FINAL_EXAM_MAX_RANGE = 200.0;
		final double FINAL_EXAM_WEIGHT = 0.25; // (grade * 0.25) is the same as (grade /200 * 50)
		final double MIN_PASSING_GRADE = 50.0;
		//weights a stored as variables, so that code can be used with other weighting sets
		
		// prompt for user input until all three fields receive proper input
		while (i < 3)
		{
			//make sure lines are spaced correctly (note Lab3.pdf shows 1 empty line at start of output)
			System.out.println();
			
			// first ask for homework grade
			if(i == 0)
			{
				System.out.println("Enter your HOMEWORK grade: ");
				homeworkGrade = scanner.nextDouble();
				
				// check if input is invalid
				if(homeworkGrade < 0 || homeworkGrade > HOMEWORK_MAX_RANGE)
				{
					System.out.println("[ERR] Invalid input. The homework grade should be in [0, 100].");
				}
				// if valid, update loop control and continue
				else
				{
					i++;
				}
			}
			// if first grade has been accepted ask for midterm grade
			else if (i == 1)
			{
				System.out.println("Enter your MIDTERM EXAM grade: ");
				midtermExamGrade = scanner.nextDouble();
				
				// check if input is invalid
				if (midtermExamGrade < 0 || midtermExamGrade > MIDTERM_EXAM_MAX_RANGE)
				{
					System.out.println("[ERR] Invalid input. A midterm grade should be in [0, 100].");
				}
				// if valid, update loop control and continue
				else
				{
					i++;
				}
			}
			// if first and second grade have been accepted, ask for final exam grade
			else
			{
				System.out.println("Enter your FINAL EXAM grade: ");
				finalExamGrade = scanner.nextDouble();
				
				// check if input is invalid
				if (finalExamGrade < 0 ||  finalExamGrade > FINAL_EXAM_MAX_RANGE)
				{
					System.out.println("[ERR] Invalid input. A final grade should be in [0, 200].");
				}
				// if valid, update loop control and continue (exit loop)
				else
				{
					// add a space after the lest input prompt
					System.out.println();
					i++;
				}
			}
		}
		
		// calculate the weighted grade total
		weightedTotal = (finalExamGrade * FINAL_EXAM_WEIGHT) + (midtermExamGrade * MIDTERM_EXAM_WEIGHT) + (homeworkGrade * HOMEWORK_WEIGHT);
		
		// show the grade total
		System.out.println("[INFO] Student's Weighted Total is " + weightedTotal);
		System.out.println();
		
		// show if the total is enough to pass the class
		if (weightedTotal >= MIN_PASSING_GRADE)
		{
			System.out.println("[INFO] Student PASSED the class");
		}
		else
		{
			System.out.println("[INFO] Student FAILED the class");
		}
		
		//Close input scanner to prevent resource leaks.
		scanner.close();
	}
}

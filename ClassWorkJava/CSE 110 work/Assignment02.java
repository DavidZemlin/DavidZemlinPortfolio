// CSE 110     : CES 110 / Online
// Assignment  : 02
// Author      : David Zemlin 1223802827
// Description : A program to calculate the materials and costs of a road construction project

import java.util.Scanner;

public class Assignment02 
{
	public static void main(String[] args) 
	{
		// declare and instantiate a Scanner
    	Scanner scan = new Scanner(System.in);

    	// input variables
		double roadLength = 0;  // in miles
		int laneCount = 0;
		int depthOfAsphalt = 0; // in inches
		int daysToComplete = 0;
		
		// internal variables
		double cubicFeetOfAsphalt = 0;
		double totalWeightOfAsphalt = 0;
		
		// outputs variables
		int truckloadsOfAsphalt = 0;
		int stoplights = 0;
		int conduitPipes = 0;
		int crewMembers = 0;
		double totalAsphaltCost = 0;
		double totalStoplightCost = 0;
		double totalConduitPipeCost = 0;
		double totalLaborCost = 0;
		double totalProjectCost = 0;
		        
		// constants
		final int TRUCK_CAPACITY = 10000;          // in lbs
		final int LANE_WIDTH = 12;                 // in ft
		final int ASPHALT_WEIGHT = 150;            // per cubic ft
		final int ASPHALT_COST = 200;              // per ton
		final int CONDUIT_PIPE_LENGTH = 24;        // in ft
		final int CONDUIT_PIPE_COST = 500;         // 24 ft sections
		final int STOPLIGHT_COST = 25000;          // per light
		final int STOPLIGHTS_PER_INTERSECTION = 2;
		final int STOPLIGHTS_PER_LANE = 1;
		final int WORK_DAY_LENGTH = 8;             // in hours
		final int HOURLY_LABOR_COST = 25;
		final int CREW_EFFICIENCY = 50;
		final double INCHES_PER_FOOT = 12;
		final double FEET_PER_MILE = 5280;
		final double POUNDS_PER_TON = 2000;
		
        // collect inputs
		System.out.println("Length of road project (miles) : ");
		roadLength = scan.nextDouble();
		System.out.println("Number of lanes                : ");
		laneCount = scan.nextInt();
		System.out.println("Depth of asphalt (inches)      : ");
		depthOfAsphalt = scan.nextInt();
		System.out.println("Days to complete project       : ");
		daysToComplete = scan.nextInt();
		
        // compute required values
		cubicFeetOfAsphalt = (roadLength * FEET_PER_MILE) * (depthOfAsphalt / INCHES_PER_FOOT) * (laneCount * LANE_WIDTH);
		totalWeightOfAsphalt = cubicFeetOfAsphalt * ASPHALT_WEIGHT;
		
		// I would have used Math.ceil() for the following rounding-up calculations instead of the "if statements", but that Math method has not been taught in our class yet.
		truckloadsOfAsphalt = (int) (totalWeightOfAsphalt / TRUCK_CAPACITY);
		if (truckloadsOfAsphalt * TRUCK_CAPACITY < totalWeightOfAsphalt)
		{
			truckloadsOfAsphalt++;
		}
		stoplights = ((int) roadLength) * (STOPLIGHTS_PER_INTERSECTION + (laneCount * STOPLIGHTS_PER_LANE));
		conduitPipes = (int) (roadLength * FEET_PER_MILE) / CONDUIT_PIPE_LENGTH;
		if (conduitPipes * CONDUIT_PIPE_LENGTH < roadLength * FEET_PER_MILE)
		{
			conduitPipes++;
		}
		crewMembers = (int) (CREW_EFFICIENCY * roadLength * laneCount) / daysToComplete;
		if (crewMembers * daysToComplete < (CREW_EFFICIENCY * roadLength * laneCount))
		{
			crewMembers++;
		}
		totalAsphaltCost = (truckloadsOfAsphalt * TRUCK_CAPACITY / POUNDS_PER_TON) * ASPHALT_COST;
		totalStoplightCost = stoplights * STOPLIGHT_COST;
		totalConduitPipeCost = conduitPipes * CONDUIT_PIPE_COST;
		totalLaborCost = HOURLY_LABOR_COST * WORK_DAY_LENGTH * crewMembers * daysToComplete;
		totalProjectCost = totalAsphaltCost + totalStoplightCost + totalConduitPipeCost + totalLaborCost;
		
        // display results
		System.out.println("=== Amount of materials needed ===");
		System.out.println("Truckloads of Asphalt : " + truckloadsOfAsphalt);
		System.out.println("Stoplights            : " + stoplights);
		System.out.println("Conduit pipes         : " + conduitPipes);
		System.out.println("Crew members needed   : " + crewMembers);
		System.out.println("=== Cost of Materials ============");
		System.out.printf("Cost of Asphalt       : $%.2f%n", totalAsphaltCost);
		System.out.printf("Cost of Stoplights    : $%.2f%n", totalStoplightCost);
		System.out.printf("Cost of Conduit pipes : $%.2f%n", totalConduitPipeCost);
		System.out.printf("Cost of Labor         : $%.2f%n", totalLaborCost);
		System.out.println("=== Total Cost of Project ========");
		System.out.printf("Total cost of project : $%.2f", totalProjectCost);
		
		//Close input to prevent resource leaks.
		scan.close();
    }

}

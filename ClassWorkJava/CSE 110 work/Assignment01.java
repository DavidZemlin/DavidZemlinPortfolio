// CSE 110     : CES 110 / Online
// Assignment  : 01
// Author      : David Zemlin 1223802827
// Description : Program that determines how slices of pizza are to be distributed among adults and children

import java.util.Scanner;

public class Assignment01
{
    public static void main(String[] args) 
    {  
        // declare and instantiate a Scanner
    	Scanner scan = new Scanner(System.in);

        // declare and initialize variables
    	int pizzasPurchased = 0;
    	int slicesPerPizza = 0;
    	int adults = 0;
    	int children = 0;
    	int totalSlices = 0;
    	int slicesForAdults = 0;
    	int slicesLeftForChildren = 0;
    	int numberOfSlicesEachChildGets = 0;
    	int slicesLeftOver = 0;
    	
        // prompt for and collect inputs
    	System.out.println("Number of pizzas purchased : ");
        pizzasPurchased = scan.nextInt();
    	System.out.println("Number of slices per pizza : ");
        slicesPerPizza = scan.nextInt();
    	System.out.println("Number of adults           : ");
        adults = scan.nextInt();
    	System.out.println("Number of children         : ");
        children = scan.nextInt();

        // determine the total number of slices
        totalSlices = pizzasPurchased * slicesPerPizza;
        
        // compute required values: first determine if there are enough slices for every adult
        if (adults * 2 < totalSlices)
        {
        	// then find out how many slices they adults take
        	slicesForAdults = adults * 2;
        	
            // next find the total number of slices left after the adults have take their share
        	slicesLeftForChildren = totalSlices - slicesForAdults;
        	
            // split the remaining whole slices evenly among the children
        	numberOfSlicesEachChildGets = slicesLeftForChildren / children;
        	
            // find how many slices are left over
        	slicesLeftOver = totalSlices - slicesForAdults - numberOfSlicesEachChildGets * children;
        }
        else
        {
        	// nothing is left for the kids thanks to those greedy adults :(
        	slicesForAdults = totalSlices;
        }
       
        // display required outputs
    	System.out.println("Total number of slices of pizza               : " + totalSlices);
    	System.out.println("Total number of slices given to adults        : " + slicesForAdults);
    	System.out.println("Total number of slices available for children : " + slicesLeftForChildren);
    	System.out.println("Number of slices each child will get          : " + numberOfSlicesEachChildGets);
    	System.out.println("Number of slices left over                    : " + slicesLeftOver);      
    	
		//Close input to prevent resource leaks.
		scan.close();
    }
}

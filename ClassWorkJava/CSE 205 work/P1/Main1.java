//********************************************************************************************************
// CLASS: Main.java
//
// DESCRIPTION
// a program that tallies the number of ascending and descending runs of numbers within a set (array list)
//
// COURSE AND PROJECT INFO
// CSE205 Object Oriented Programming and Data Structures, Fall 2021
// Project Number: 01
// 
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//********************************************************************************************************
import java.util.ArrayList;
import java.util.Scanner;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;

public class Main1 
{
    
    // main method (just activates the first program)
    public static void main(String[] pArgs)
    {
        Main1 mainObject = new Main1();
        mainObject.run();
    }
    
    // run method
    // takes in a file of positive integers and writes a new file outlining how many runs are present in the array of ints and how long each run is
    private void run()
    {
        final boolean RUNS_UP = true;
        final boolean RUNS_DOWN = false;
        int runsTotal;
        System.out.println("Run Program...");
        ArrayList<Integer> list = readArrayOfInts("p01-in.txt");
        ArrayList<Integer> upList = findRuns(list, RUNS_UP);
        ArrayList<Integer> downList = findRuns(list, RUNS_DOWN);
        ArrayList<Integer> mergedList = mergeLists(upList, downList);
        runsTotal = sumOfIntList(mergedList);
        writeOutputFile("p01-runs.txt", mergedList, runsTotal);
    }
    
    /**
     * Searches a given list of Integers for ascending or descending runs, and provides an array list of the count of each length of run.
     * @param pList list that will be searched for runs
     * @param pGoingUp direction of runs to be searched for: true = ascending; false = descending
     * @return an array list that counts how many runs of each length were found; listed in ascending order of run length
     */
    private ArrayList<Integer> findRuns(ArrayList<Integer> pList, boolean pGoingUp)
    {
        // set up a new array with one less than the length of pList initialized to 0 for each element
        ArrayList<Integer> listRunCounts = new ArrayList<>();
        for (int i = 0; i < pList.size() -1; i++)
        {
            listRunCounts.add(0);
        }
        // find the total number of each run of a specific length in the array, in a given direction
        int runLength = 0;
        for (int i = 0; i < pList.size() - 1; i++)
        {
            // if looking for ascending runs
            if (pGoingUp == true && pList.get(i) <= pList.get(i + 1))
            {
                runLength++;
            }
            // if looking for descending runs
            else if (pGoingUp == false && pList.get(i) >= pList.get(i + 1))
            {
                runLength++;
            }
            // this element is not running
            else
            {
                if (runLength != 0)
                {
                    listRunCounts.set(runLength - 1, listRunCounts.get(runLength - 1) + 1);
                    runLength = 0;
                }
            }
        }
        // catch the last run that did not close out
        if (runLength != 0)
        {
            listRunCounts.set(runLength - 1, listRunCounts.get(runLength -1 ) + 1);
        }
        return listRunCounts;
    }
    
    /**
     * Reads an input file and records all ints found within.
     * @param pFileName the file name to be opened
     * @return an ArrayList of Integers found in the file
     */
    private ArrayList<Integer> readArrayOfInts(String pFileName)
    {
        ArrayList<Integer> intList = new ArrayList<>();
        boolean forceClose = false;
        
        // if file can be opened...
        try (FileReader reader = new FileReader(pFileName); Scanner inFile = new Scanner(reader))
        {
            // check for the next item in the file...
            while (inFile.hasNext())
            {
                // ... then determine if the item qualifies as a positive integer...
                String next = inFile.next();
                boolean isInt = true;
                for (int i = 0; i < next.length() && isInt == true; i++)
                {
                    if (!Character.isDigit(next.charAt(i)))
                    {
                        isInt = false;
                    }
                }
                // ... and add it to the list if it is an int
                if (isInt)
                {
                    intList.add(Integer.parseInt(next));
                }
            }           
        }
        // ... if not, force the program to quit if file fails to open
        catch (IOException ex)
        {
            forceClose = true;
        }
        if(forceClose)
        {
            System.out.println("Oops, could not open 'p01-in.txt' for reading. The program is ending.");
            System.exit(-100);
        }
        
        // return the new ArrayList
        return intList; 
    }
    
    /**
     * Creates a .txt file with the first line being the total runs and each subsequent line showing how many runs of a particular run were recorded in the given array.
     * @param pFileName the desired output file name
     * @param pList the list of runs to be printed
     * @param pRunsTotal total number of runs to be displayed on the first line
     */
    private void writeOutputFile(String pFileName, ArrayList<Integer> pList, int pRunsTotal)
    {
        boolean forceClose = false;
        // try to open or create the write file ...
        try (PrintWriter write = new PrintWriter(pFileName))
        {
            // print each line
            write.println("runs_total: " + pRunsTotal);
            for (int i = 0; i < pList.size(); i++)
            {
                write.println("runs_" + (i + 1) + ": " + pList.get(i));
            }
        }
        // ... if it fails, force the program to quit
        catch (IOException ex)
        {
            forceClose = true;
        }
        if(forceClose)
        {
            System.out.println("Oops, could not open 'p01-runs.txt' for writing. The program is ending.");
            System.exit(-200);
        }
    }
    
    /**
     * Merges two integer array lists together, adding together the values of elements that have the same index and keeping the same list size as the larger of the two lists.
     * @param pListA first list to be merged
     * @param pListB second list to be merged
     * @return the combined list
     */
    private ArrayList<Integer> mergeLists(ArrayList<Integer> pListA, ArrayList<Integer> pListB)
    {
        ArrayList<Integer> result = new ArrayList<>();
        int newListSize = pListA.size();
        if (pListA.size() < pListB.size())
        {
            newListSize = pListB.size();
        }
        for (int i = 0; i < newListSize; i++)
        {
            result.add(pListA.get(i) + pListB.get(i));
        }
        return result;
    }
    
    /**
     * Adds together all elements of an Integer Array List.
     * @param pList list to be summed up
     * @return total of all integers in the array list
     */
    private int sumOfIntList(ArrayList<Integer> pList)
    {
        int sum = 0;
        for (int i = 0; i < pList.size(); i++)
        {
            sum += pList.get(i);
        }
        return sum;
    }
}
// CSE 110     : CES 110 / Online
// Assignment  : 06
// Author      : David Zemlin 1223802827
// Description : collection of 10 methods for implementing arrays

public class Assignment06
{

    public static void main(String[] args)
    {
        // TA code testing calls
    	int[] myArray = {1, 22, 333, 400, 5005, 9};
    	printArray(myArray, ", ");
    	printArray(myArray, " - ");
    	getFirst(myArray);
    	getLast(myArray);
    	getAllButFirst(myArray);
    	getIndexOfMin(myArray);
    	getIndexOfMax(myArray);
    	swapByIndex(myArray, 1, 4);
    	removeAtIndex(myArray, 3);
    	insertAtIndex(myArray, 2, 777);
    	isSorted(myArray);
    }
    
    /**
     * Prints out a given array with a custom separator between the values.
     * @param array the array to be printed.
     * @param separator the separator to be used.
     */
    public static void printArray(int[] array, String separator)
    {
    	for (int i = 0; i < array.length; i++)
    	{
    		System.out.print(array[i]);
    		if (i != array.length - 1)
    		{
    			System.out.print(separator);
    		}
    	}
    	System.out.println();
    }
    
    /**
     * Finds and returns the First element in a given array.
     * @param array the array to be searched.
     * @return the value of the first element of the array.
     */
    public static int getFirst(int[] array)
    {
    	return array[0];
    }
    
    /**
     * Finds and returns the last element in a given array.
     * @param array the array to be searched.
     * @return the value of the last element of the array.
     */
    public static int getLast(int[] array)
    {
    	return array[array.length - 1];
    }
    
    /**
     * Makes a new array based on a given array, but with the first element removed.
     * @param array the array the new one will be based on.
     * @return the new array minus the first element of the given array.
     */
    public static int[] getAllButFirst(int[] array)
    {
    	int[] result = new int[array.length - 1];
    	for (int i = 1; i < array.length; i++)
    	{
    		result[i - 1] = array[i];
    	}
    	return result;
    }
    
    
    /**
     * Finds the index of the lowest value in an array.
     * @param array the array to be searched.
     * @return the index of the lowest value.
     */
    public static int getIndexOfMin(int[] array)
    {
    	int min = 0;
    	for (int i = 1; i < array.length; i++)
    	{
    		if (array[i] < array[min])
    		{
    			min = i;
    		}
    	}
    	return min;
    }
    
    /**
     * Finds the index of the greatest value in an array.
     * @param array the array to be searched.
     * @return the index of the greatest value.
     */
    public static int getIndexOfMax(int[] array)
    {
    	int max = 0;
    	for (int i = 1; i < array.length; i++)
    	{
    		if (array[i] > array[max])
    		{
    			max = i;
    		}
    	}
    	return max;
    }
    
    /**
     * Swaps the position of 2 given array elements and returns the reference of the array.
     * @param array the array that will have its elements swapped.
     * @param index1 index of the 1st element to be swapped.
     * @param index2 index of the 2nd element to be swapped.
     * @return the reference of the array that was altered.
     */
    public static int[] swapByIndex(int[] array, int index1, int index2)
    {
    	int temp = array[index1]; // place to temporarily store the value of the first index
    	array[index1] = array[index2];
    	array[index2] = temp;
    	return array;
    }
    
    /**
     * Makes a new array with a value removed from a given point.
     * @param array an array of integers to remove the value into.
     * @param index the position to be removed.
     * @return the new array with the removed value.
     */
    public static int[] removeAtIndex(int[] array, int index)
    {
    	int[] result = new int[array.length - 1];
    	int count = 0; // counter for elements placed into new array
    	for (int i = 0; i < array.length; i++)
    	{
    		if (i != index)
    		{
    			result[count] = array[i];
    			count++;
    		}
    	}
    	return result;
    }
    
    /**
     * Makes a new array with a value inserted at a given point.
     * @param array an array of integers to insert the value into.
     * @param index the position the new value will be inserted at.
     * @param value the value to be inserted.
     * @return the new array with the inserted value.
     */
    public static int[] insertAtIndex(int[] array, int index, int value)
    {
    	int[] result = new int[array.length + 1];
    	int count = 0; // counter for new elements inserted into the array
    	for (int i = 0; i < result.length; i++)
    	{
    		if (i == index)
    		{
    			result[i] = value;
    			count++;
    		}
    		else
    		{
    			result[i] = array[i - count];
    		}
    	}
    	return result;
    }
    
    /**
     * Returns true if all elements in the array are sorted in ascending order.
     * @param array the array to be checked for sorting.
     * @return true if the array is sorted; false if not.
     */
    public static boolean isSorted(int[] array)
    {
    	for (int i = 0; i < array.length - 1; i++)
    	{
    		if (array[i] > array[i + 1])
    		{
    			return false;
    		}
    	}
    	return true;
    }
    

}

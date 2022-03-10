//**************************************************************************************************
// CLASS: Sorter
//
// DESCRIPTION
// Sorts the roster of students
//
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 03
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//**************************************************************************************************
package proj3;

import java.util.ArrayList;

/**
 * Sorts the roster of student.
 */
public class Sorter {
    /**
     * Partitions an part of an array list of students
     * @param pList list to be partitioned
     * @param pFromIdx start index of the partition
     * @param pToIdx end index of the partition
     * @return the end index of the first partition
     */
    private static int partition(ArrayList<Student> pList, int pFromIdx, int pToIdx) {
        
        Student pivot = pList.get(pFromIdx);
        int i = pFromIdx - 1;
        int k = pToIdx + 1;
        while (i < k)
        {
            i++;
            while (pList.get(i).compareTo(pivot) < 0) {
                i++;
            }
            k--;
            
            while (pList.get(k).compareTo(pivot) > 0) {
                k--;
            }
            
            if (i < k) {
                swap(pList, i, k);
            }
        }
        return k;
    }
    
    /**
     * Sorts a given part of on array list in a speedy way.
     * @param pList the array list to be sorted
     * @param pFromIdx the start index of the section to be sorted
     * @param pToIdx the end index of the section to be sorted
     */
    private static void quickSort(ArrayList<Student> pList, int pFromIdx, int pToIdx) {
        if (pFromIdx >= pToIdx) {
            return;
        }
        int parti = partition(pList, pFromIdx, pToIdx);
        quickSort(pList, pFromIdx, parti);
        quickSort(pList, parti + 1, pToIdx);
    }
    
    /**
     * Sorts an array list using a quick sort algorithm.
     * @param pList list to be sorted
     */
    public static void sort(ArrayList<Student> pList) {
        quickSort(pList, 0, pList.size() - 1);
    }
    
    /**
     * Swaps two elements of an array list
     * @param pList the array that contains the elements to be swapped.
     * @param pIdx1 the first element to be swapped
     * @param pIdx2 the second element to be swapped
     */
    private static void swap(ArrayList<Student> pList, int pIdx1, int pIdx2) {
        Student temp = pList.get(pIdx1);
        pList.set(pIdx1, pList.get(pIdx2));
        pList.set(pIdx2, temp);
    }
}

//**************************************************************************************************
// CLASS: Searcher
//
// DESCRIPTION
// Searches the roster for a student
//
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 03
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//**************************************************************************************************
package proj3;

import java.util.ArrayList;

/**
 * Searches the roster for a student.
 */
public class Searcher {
    /**
     * Binary search iterating though a list of students, searching based on the last name.
     * @param pList the list to be searched
     * @param pKey the last name we are looking for (not case sensitive)
     * @return the index of the student with the given last name. returns -1 if the student is not found
     */
    public static int search(ArrayList<Student> pList, String pKey) {
        int low = 0;
        int high = pList.size() - 1;
        
        while (low <= high) {
            int mid = (low + high) / 2;
            int comparisonValue = pKey.compareToIgnoreCase(pList.get(mid).getLastName());
            if (comparisonValue == 0) {
                return mid;
            }
            else if (comparisonValue < 0) {
                high = mid -1;
            }
            else {
                low = mid + 1;
            }
            
        }
        return -1;
    }
}

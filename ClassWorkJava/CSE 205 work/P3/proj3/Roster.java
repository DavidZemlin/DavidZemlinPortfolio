//**************************************************************************************************
// CLASS: Roster
//
// DESCRIPTION
// Contains and manages a list all students in a course
//
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 03
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//
// ORIGINAL AUTHOR (This is a file edited form an original file with a different author)
// Kevin R. Burger (burgerk@asu.edu)
// Computer Science & Engineering Program
// Fulton Schools of Engineering
// Arizona State University, Tempe, AZ 85287-8809
// (c) Kevin R. Burger 2014-2019
//**************************************************************************************************
package proj3;

import java.util.ArrayList;

/**
 * The Roster class encapsulates an ArrayList<Student> object named mStudentList which stores the
 * information for each student that was read from "gradebook.dat" when the app started.
 */
public class Roster {

    /**
     * Declare mStudentList as ArrayList<Student>
     */
    private ArrayList<Student> mStudentList;

    /**
     * Roster Constructor
     */
    public Roster() {
        setStudentList(new ArrayList<Student>());
    }

    /**
     * addStudent()
     *
     * Adds a student to the ArrayList
     */
    public void addStudent(Student pStudent) {
        mStudentList.add(pStudent);
    }

    /**
     * getStudent()
     *
     * Searches mStudentList for a Student with pLastName.
     * @param pLastName last name of the student we are searching for
     * @return the found student's index within the array
     */
    public Student getStudent(String pLastName) {
        int index = Searcher.search(getStudentList(), pLastName);
        if (index < 0) {
            return null;
        }
        else {
            return getStudentList().get(index);
        }
    }

    /**
     * getStudentList()
     *
     * Accessor method for mStudentList.
     *
     * Note: it is extremely sleazy to provide public access to the entire private student list
     * (mStudentList) in this way because it gives whoever calls this method the ability to
     * modify any Student in the roster. It would be better to have the Roster class implement an
     * iterator that would permit other objects to iterate over the elements of the list, but in an
     * effort to keep the project as simple as possible, I am taking the sleazy route.
     *
     * If you are so inclinded, by all means, implement the iterator.
     */
    public ArrayList<Student> getStudentList() {
        return mStudentList;
    }

    /**
     * setStudentList()
     *
     * Mutator method for mStudentList.
     */
    private void setStudentList(ArrayList<Student> pStudentList) {
        mStudentList = pStudentList;
    }

    /**
     * sortRoster()
     * Called to sort the roster by last name.
     */
    public void sortRoster() {
        Sorter.sort(getStudentList());
    }

    /**
     * Returns a String representation of this Roster. toString() methods are very handy for
     * debugging because given access to a Roster object, say named roster, then you can print
     * the entire roster in one statement: System.out.println(roster);
     * @return a String representation of this Roster
     */
    @Override
    public String toString() {
        String result = "";
        for (Student student : getStudentList()) {
            result += student + "\n";
        }
        return result;
    }
}

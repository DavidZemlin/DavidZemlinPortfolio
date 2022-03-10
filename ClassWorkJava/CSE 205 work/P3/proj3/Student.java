//**************************************************************************************************
// CLASS: Student
//
// DESCRIPTION
// Stores and manages the student information and objects for the gradebook
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
 * The Student class stores the gradebook information for one Student.
 *
 * Note that Student implements the java.lang.Comparable<Student> interface because we are going
 * to be sorting the roster of students by last name so we need to be able to compare the last
 * names of two students A and B to determine if A < B, or if A = B, or if A > B. See the
 * compareTo() method.
 */
public class Student implements Comparable<Student> {

    /**
     * mCurrStudent is a reference to the Student object which is currently being displayed and
     * edited in the View. It should only be accessed via accessor/mutator methods.
     */
    private static Student mCurrStudent;
    
    /**
     * mExamList is an ArrayList of Integers storing the student's exam scores.
     */
    private ArrayList<Integer> mExamList;
    
    /**
     * The student's first name.
     */
    private String mFirstName;

    /**
     * mHomework List is an ArrayList of Integers storing the student's homework scores.
     */
    private ArrayList<Integer> mHomeworkList;

    /**
     * The student's last name.
     */
    private String mLastName;

    /**
     * Student() Constructor.
     * @param pFirstName first name of the student
     * @param pLastName last name of the student
     */
    Student(String pFirstName, String pLastName) {
        setFirstName(pFirstName);
        setLastName(pLastName);
        setExamList(new ArrayList<Integer>());
        setHomeworkList(new ArrayList<Integer>());
    }

    /**
     * addExam()
     *
     * Adds an exam score pScore to the exam list
     *
     * @param pScore
     */
    public void addExam(int pScore) {
        getExamList().add(pScore);
    }

    /**
     * addHomework()
     *
     * Adds a homework score pScore to the homework list
     *
     * @param pScore
     */
    public void addHomework(int pScore) {
        getHomeworkList().add(pScore);
    }

    /**
     * compareTo()
     *
     * @param pStudent is a Student
     * @return -1 if this name comes before the other name, 1 if not and 0 if the names are the same
     *
     * This method compares the last name of 'this' Student to the last name of pStudent to
     * determine if the last name of 'this' Student is <, =, or > the last name of pStudent.
     * It is called during the quick sort routine in Sorter.partition().
     *
     * Provide the annotation that prevents accidental overloading since we are overriding the
     * String.compareTo() method.
     */
    @Override
    public int compareTo(Student pStudent) {
        int compareValue = getLastName().compareToIgnoreCase(pStudent.getLastName());
        return compareValue;
    }
    
    /**
     * Accessor method for mCurrStudent.
     */ 
    public static Student getCurrStudent() {
        return mCurrStudent;
    }

    /**
     * getExam()
     *
     * Accessor method to retrieve an exam score from the list of exams.
     *
     * @param pNum The exam number for which we wish to retrieve the score.
     * @return The exam score.
     */
    public int getExam(int pNum) {
        return getExamList().get(pNum);
    }

    /**
     * getExamList()
     *
     * Accessor method for mExamList.
     * @return the exam list
     */
    private ArrayList<Integer> getExamList() {
        return mExamList;
    }

    /**
     * getFirstName()
     *
     * Accessor method for mFirstName.
     * @return first name
     */
    public String getFirstName() {
        return mFirstName;
    }

    /**
     * getFullName()
     *
     * Returns the student's full name in the format: "lastname, firstname".
     * @return students full name
     */
    public String getFullName() {
        return mLastName + ", " + mFirstName;
    }
    
    /**
     * getHomework()
     *
     * Accessor method to retrieve a homework score from the list of homeworks.
     *
     * @param pNum The homework number for which we wish to retrieve the score.
     * @return The homework score.
     */
    public int getHomework(int pNum) {
        return getHomeworkList().get(pNum);
    }

    /**
     * getHomeworkList()
     *
     * Accessor method for mHomeworkList.
     * @return homework list
     */
    private ArrayList<Integer> getHomeworkList() {
        return mHomeworkList;
    }

    /**
     * getLastname()
     *
     * Accessor method for mLastName.
     * @return last name
     */
    public String getLastName() {
        return mLastName;
    }

    /**
     * Mutator method for mCurrStudent.
     */ 
    public static void setCurrStudent(Student pCurrStudent) {
        mCurrStudent = pCurrStudent;
    }

    /**
     * setExam()
     *
     * Mutator method to store an exam score into the list of exam scores.
     *
     * @pNum is the index into the list of exams, where 0 is the index of the first exam score.
     * @pScore the new score for the given exam
     */
    public void setExam(int pNum, int pScore) {
        getExamList().set(pNum, pScore);
    }

    /**
     * setExamList()
     *
     * Mutator method for mExamList.
     */
    private void setExamList(ArrayList<Integer> pExamList) {
        mExamList = pExamList;
    }

    /**
     * setFirstName()
     *
     * Mutator method for mFirstName.
     */
    public void setFirstName(String pFirstName) {
        mFirstName = pFirstName;
    }

    /**
     * setHomework()
     *
     * Mutator method to store a homework score into the list of homework scores.
     *
     * @pNum is the index into the list of homeworks, where 0 is the index of the first hw score.
     * @pScore the new score for the given homework assignment
     */
    public void setHomework(int pNum, int pScore) {
        getHomeworkList().set(pNum, pScore);
    }

    /**
     * setHomeworkList()
     *
     * Mutator method for mHomeworkList.
     * @param pHomeworkList new list
     */
    private void setHomeworkList(ArrayList<Integer> pHomeworkList) {
        mHomeworkList = pHomeworkList;
    }

    /**
     * setLastname()
     *
     * Mutator method for mLastName.
     * @param pLastName last name
     */
    public void setLastName(String pLastName) {
        mLastName = pLastName;
    }

    /**
     * toString()
     *
     * Returns a String representation of this Student. The format of the returned string shall be
     * such that the Student information can be printed to the output file in this format:
     *
     *     lastname firstname exam1 exam2 exam2 hw1 hw2 hw3 hw4 hw5
     *
     * where the fields are separated by spaces, except there is not space following hw5.
     */
    @Override
    public String toString() {
        String result = getLastName() + " " + getFirstName() + " ";
        for (Integer exam : getExamList()) {
            result += exam + " ";
        }
        for (Integer hw : getHomeworkList()) {
            result += hw + " ";
        }
        return result.trim();
    }
}

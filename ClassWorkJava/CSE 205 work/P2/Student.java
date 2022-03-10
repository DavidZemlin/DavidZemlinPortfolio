//**************************************************************************************************
// CLASS: Student
//
// DESCRIPTION
// Student is an abstract class and is the superclass for the OnCampusStudent and OnlineStudent
// classes.
//
// COURSE AND PROJECT INFO
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 02
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//
// ORIGINAL AUTHOR (This is a file edited form an original file with a different author)
// Kevin R. Burger (burgerk@asu.edu)
// Computer Science & Engineering
// School of Computing, Informatics, and Decision Systems Engineering
// Fulton Schools of Engineering
// Arizona State University, Tempe, AZ 85287-8809
//**************************************************************************************************
public abstract class Student implements Comparable<Student> {

    private int mCredits;
    private String mFirstName;
    private String mId;
    private String mLastName;
    private double mTuition;
    
    /**
     * Creates a Student object and initializes the mId, mFirstName, and mLastName instance
     * variables.
     */
    public Student(String pId, String pFirstName, String pLastName) {
        mId = pId;
        mFirstName = pFirstName;
        mLastName = pLastName;
    }

    /**
     * calcTuition() is to be implemented by subclasses of Student. Will be used to calculate the
     * total tuition of a student.
     */
    public abstract void calcTuition();
    
    /**
     * Compares the mId fields of 'this' Student and pStudent. Returns -1 if this Student's mId
     * field is less than pStudent's mId field. Returns 0 if this Student's mId field is equal to
     * pStudent's mId field. Returns 1 if this Student's mId field is greater than pStudent's mId
     * field.
     */
    @Override
    public int compareTo(Student pStudent) {
        return getId().compareTo(pStudent.getId());
    }

    /**
     * Accessor method for mCredits.
     */
    public int getCredits() {
        return mCredits;
    }

    /**
     * Accessor method for mFirstName.
     */
    public String getFirstName() {
        return mFirstName;
    }

    /**
     * Accessor method for mId.
     */
    public String getId() {
        return mId;
    }

    /**
     * Accessor method for mLastName.
     */
    public String getLastName() {
        return mLastName;
    }

    /**
     * Accessor method for mTuition.
     */
    public double getTuition() {
        return mTuition;
    }

    /**
     * Mutator method for mCredits.
     */
    public void setCredits(int pCredits) {
        mCredits = pCredits;
    }

    /**
     * Mutator method for mFirstName.
     */
    public void setFirstName(String pFirstName) {
        mFirstName = pFirstName;
    }

    /**
     * Mutator method for mId.
     */
    public void setId(String pId) {
        mId = pId;
    }

    /**
     * Mutator method for mLastName.
     */
    public void setLastName(String pLastName) {
        mLastName = pLastName;
    }

    /**
     * Mutator method for mTuition.
     */
    protected void setTuition(double pTuition) {
        mTuition = pTuition;
    }

}

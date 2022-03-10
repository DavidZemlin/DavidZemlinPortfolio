//**************************************************************************************************
// CLASS: OnCampusStudent
//
// DESCRIPTION
// A concrete subclass of student that represents an On Campus Student's data and relevant methods.
//
// COURSE AND PROJECT INFO
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 02
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//**************************************************************************************************

public class OnCampusStudent extends Student {
    public final int RESIDENT = 1;
    public final int NON_RESIDENT = 2;
    
    private int mResident;
    private double mProgramFee;
    
    /**
     * Creates an OnCampusStudent object, initializing the mId, mFirstName, and mLastName instance
     * variables of the superclass.
     */
    public OnCampusStudent(String pId, String pFirstName, String PLastName) {
        super(pId, pFirstName, PLastName);
    }

    /**
     * Calculates the total tuition for this student
     */
    @Override
    public void calcTuition() {
        double t;
        if (getResidency() == RESIDENT) {
            t = TuitionConstants.ONCAMP_RES_BASE;
        }
        else {
            t = TuitionConstants.ONCAMP_NONRES_BASE;
        }
        t += getProgramFee();
        if (getCredits() > TuitionConstants.ONCAMP_MAX_CREDITS) {
            t += (getCredits() - TuitionConstants.ONCAMP_MAX_CREDITS) * TuitionConstants.ONCAMP_ADD_CREDITS;
        }
        setTuition(t);
    }
    
    /**
     * Accessor method for mProgramFee.
     */
    public double getProgramFee() {
        return mProgramFee;
    }
    
    /**
     * Accessor method for mResident.
     */
    public int getResidency() {
        return mResident;
    }
    
    /**
     * Mutator method for mProgramFee.
     */
    public void setProgramFee(double pProgramFee) {
        mProgramFee = pProgramFee;
    }
    
    /**
     * Mutator method for mResident.
     */
    public void setResidency(int pResident) {
        mResident = pResident;
    }
}

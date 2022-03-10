//**************************************************************************************************
// CLASS: OnlineStudent
//
// DESCRIPTION
// A concrete subclass of student that represents an Online Student's data and relevant methods.
//
// COURSE AND PROJECT INFO
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 02
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//**************************************************************************************************

public class OnlineStudent extends Student {
    private boolean mTechFee;
    
    /**
     * Creates an OnlineStudent object, initializing the mId, mFirstName, and mLastName instance
     * variables of the superclass.
     */
    public OnlineStudent(String pId, String pFirstName, String pLastName) {
        super(pId, pFirstName, pLastName);
    }

    /**
     * Calculates the total tuition for this student
     */
    @Override
    public void calcTuition() {
        double t = getCredits() * TuitionConstants.ONLINE_CREDIT_RATE;
        if (getTechFee()) {
            t += TuitionConstants.ONLINE_TECH_FEE;
        }
        setTuition(t);
    }
    
    /**
     * Accessor method for mTechFee.
     */
    public boolean getTechFee() {
        return mTechFee;
    }
    
    /**
     * Mutator method for mTechFee.
     */
    public void setTechFee(boolean pTechFee)
    {
        mTechFee = pTechFee;
    }
}

//**************************************************************************************************************
// CLASS: Operand
//
// COURSE AND PROJECT INFO
// CSE205 Object Oriented Programming and Data Structures, Fall B and 2021
// Project Number: 04
//
// AUTHOR: David Zemlin, dzemlin, dzemlin@asu.edu
//
// ORIGINAL AUTHOR (This is a file edited form an original file with a different author)
// Kevin R. Burger (burgerk@asu.edu)
// Computer Science & Engineering Program
// Fulton Schools of Engineering
// Arizona State University, Tempe, AZ 85287-8809
// http://www.devlang.com
//**************************************************************************************************************
package p04;

/**
 * An operand is a numeric value represented as a Double.
 */
public class Operand extends Token {
    
    Double mValue;
    
    /**
     * Constructor. Creates an operand set to a certain value.
     * @param pValue the value of the operand
     */
    public Operand(Double pValue) {
        setValue(pValue);
    }
    
    /**
     * Accessor method for mValue.
     * @return the value of mValue
     */
    public Double getValue() {
        return mValue;
    }
    
    /**
     * Mutator method for mValue.
     * @param pValue the new value
     */
    public void setValue(Double pValue) {
        mValue = pValue;
    }
}

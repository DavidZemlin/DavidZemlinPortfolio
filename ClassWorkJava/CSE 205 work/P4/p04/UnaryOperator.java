//**************************************************************************************************************
// CLASS: UnaryOperator
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
 * UnaryOperator is the superclass of all unary operators.
 */
public abstract class UnaryOperator extends Operator {

    public UnaryOperator() {
    }
    
    /**
     * Called to evaluate the operator.
     */
    public abstract Operand evaluate(Operand pOperand);

    /**
     * Returns false since all subclasses of UnaryOperator are unary operators.
     */
    @Override
    public boolean isBinaryOperator() {
        return false;
    }

}

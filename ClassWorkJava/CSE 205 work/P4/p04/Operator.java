//**************************************************************************************************************
// CLASS: Operator
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
 * Operator is the superclass of all binary and unary operators.
 */
public abstract class Operator extends Token {
    
    
    public Operator() { }
    
    // abstract methods for implementation in children
    public abstract boolean isBinaryOperator();
    
    public abstract int precedence();
    
    public abstract int stackPrecedence();
}

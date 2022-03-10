//**************************************************************************************************************
// CLASS: Expression
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
 * Represents an infix expression to be evaluated.
 */
public class Expression {

    /**
     * A queue which stores the tokens of the infix expression in the order in which they were input.
     */
    Queue<Token> mTokenQueue;

    /**
     * Expression(String)
     *
     * pExprStr is a string representing an infix expression, such as "(1 + 2) * -3". This ctor uses the
     * Tokenizer class to break the string into Token objects which are stored in the token queue instance
     * variable.
     */
    public Expression(String pExprStr) {
        setTokenQueue(new Queue<Token>());
        Tokenizer tokenizer = new Tokenizer(pExprStr);
        Token prevToken = null;
        Token token = tokenizer.nextToken();
        while (token != null)
        {
            if (token instanceof SubOperator) {
                token = negationCheck(token, prevToken);
            }
            getTokenQueue().enqueue(token);
            prevToken = token;
            token = tokenizer.nextToken();
        }
    }

    /**
     * Evaluates the expression and returns the result as a Double.
     */
    public double evaluate() {
        Stack<Operator> operatorStack = new Stack<>();
        Stack<Operand> operandStack = new Stack<>();
        while (!mTokenQueue.isEmpty()) {
            Token token = getTokenQueue().dequeue();
            if (token instanceof Operand) {
                operandStack.push((Operand) token);
            }
            else if (token instanceof LeftParen) {
                operatorStack.push((LeftParen) token);
            }
            else if (token instanceof RightParen) {
                while (!(operatorStack.peek() instanceof LeftParen)) {
                    topEval(operatorStack, operandStack);
                }
                operatorStack.pop();
            }
            else {
                Operator operator = (Operator) token;
                while (keepEvaluating(operatorStack, operator)) {
                    topEval(operatorStack, operandStack);
                }
                operatorStack.push(operator);
            }
        }
        while (!operatorStack.isEmpty()) {
            topEval(operatorStack, operandStack);
        }
        return operandStack.pop().getValue();
    }

    /**
     * Accessor method for mTokenQueue.
     */
    protected Queue<Token> getTokenQueue() {
        return mTokenQueue;
    }

    /**
     * Returns true when we need to pop the operator on top of the operator stack and evaluate it. If the stack
     * is empty, returns false since there is no operator to pop. Otherwise, return true if the operator on top
     * of the operator stack has stack precedence greater than or equal to the normal precedence of pOperator.
     */
    private boolean keepEvaluating(Stack<Operator> pOperatorStack, Operator pOperator) {
        if (pOperatorStack.isEmpty()) {
            return false;
        } else {
            return pOperatorStack.peek().stackPrecedence() >= pOperator.precedence();
        }
    }

    /**
     * Since the negation and subtraction operators look the same we can identify negation when:
     *
     * 1. The previous pToken is null (negation can be the first operator in an expression but sub cannot)
     * 2. Or if the previous pToken was a binary operator (sub cannot be preceded by another binary operator)
     * 3. Or if the previous pToken was a left parenthesis (sub cannot be preceded by a left paren)
     *
     * This method determines if pToken is really a negation operator rather than a subtraction operator, and if
     * so, will return a negation operator pToken. If pToken represents subtraction, then we simply return pToken.
     */
    private Token negationCheck(Token pToken, Token pPrevToken) {
        if (pPrevToken == null || pPrevToken instanceof BinaryOperator || pPrevToken instanceof LeftParen) {
            pToken = new NegOperator();
        }
        return pToken;
    }

    /**
     * Mutator method for mTokenQueue.
     */
    protected void setTokenQueue(Queue<Token> pTokenQueue) {
        mTokenQueue = pTokenQueue;
    }

    /**
     * topEval()
     *
     * Evaluates the "top" of the stack. If the top operator on the operator stack is a unary operator, we pop
     * one operand from the operand stack, evaluate the result, and push the result onto the operand stack. If
     * the top operator on the operator stack is a binary operator, we pop two operands from the operand stack,
     * evaluate the result of the operation, and push the result onto the operand stack.
     */
    private void topEval(Stack<Operator> pOperatorStack, Stack<Operand> pOperandStack) {
        Operand right = pOperandStack.pop();
        Operator operator = pOperatorStack.pop();
        if (operator instanceof UnaryOperator) {
            Operand result = ((UnaryOperator) operator).evaluate(right);
            pOperandStack.push(result);
        }
        else {
            Operand left = pOperandStack.pop();
            Operand result = ((BinaryOperator) operator).evaluate(left, right);
            pOperandStack.push(result);
        }
    }

}

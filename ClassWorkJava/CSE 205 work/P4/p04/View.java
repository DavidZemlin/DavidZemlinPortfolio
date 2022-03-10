//**************************************************************************************************************
// CLASS: View
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
// Web: http://www.devlang.com
//**************************************************************************************************************
package p04;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.Box;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTextField;

/**
 * The View class implements the GUI.
 */
public class View extends JFrame implements ActionListener {

    public static final int FRAME_WIDTH  = 500;
    public static final int FRAME_HEIGHT = 180;

    // Declare instance variables
    private JButton         mClearButton;
    private JButton         mEvalButton;
    private JTextField      mInputText;
    private JButton         mExitButton;
    private Main            mMain;
    private JLabel          mResultLabel;

    /**
     * View()
     *
     * The View constructor creates the GUI interface and makes the frame visible at the end.
     */
    public View(Main pMain) {
        // Save a reference to the Main object pMain in mMain.
        mMain = pMain;

        JPanel panelLabel = new JPanel();
        mResultLabel = new JLabel("");
        panelLabel.add(mResultLabel);

        JPanel panelInput = new JPanel();
        mInputText = new JTextField(40);
        panelInput.add(mInputText);

        JPanel panelButtons = new JPanel();
        mClearButton = new JButton("Clear");
        mEvalButton = new JButton("Evalute");
        mExitButton = new JButton("Exit");
        mClearButton.addActionListener(this);
        mEvalButton.addActionListener(this);
        mExitButton.addActionListener(this);
        panelButtons.add(mClearButton);
        panelButtons.add(mEvalButton);
        panelButtons.add(mExitButton);

        JPanel panelMain = new JPanel();
        panelMain.setLayout(new BoxLayout(panelMain, BoxLayout.Y_AXIS));
        panelMain.add(Box.createVerticalGlue());
        panelMain.add(panelLabel);
        panelMain.add(panelInput);
        panelMain.add(panelButtons);

        setTitle("Kalkutron-9001");
        setSize(FRAME_WIDTH, FRAME_HEIGHT);
        setResizable(false);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(panelMain);
        setVisible(true);
    }

    /**
     * actionPerformed()
     *
     * Called when one of the JButtons is clicked. Detects which button was clicked and handles it.
     */
    public void actionPerformed(ActionEvent pEvent) {
        if (pEvent.getSource().equals(mClearButton)) {
            clear();
        }
        else if (pEvent.getSource().equals(mEvalButton)) {
            evaluate();
        }
        else if (pEvent.getSource().equals(mExitButton)) {
            mMain.exit();
        }
    }

    /**
     * clear() is called when the Clear button is clicked. Set the text in mInputText and mResultLabel to the
     * empty strings "".
     */
    private void clear() {
        mInputText.setText("");
        mResultLabel.setText("");
    }

    /**
     * evaluate() is called when the Evaluate button is clicked.
     */
    private void evaluate() {
        Expression expr = new Expression(mInputText.getText());
        Double result = expr.evaluate();
        mResultLabel.setText(result.toString());
    }

    /**
     * messageBox()
     *
     * Note that passing 'this' as the first arg causes the View to be the parent of the message
     * dialog window, so the dialog will be centered in the middle of the View. If we pass 'null'
     * as the argument, then the dialog does not have a parent frame and will be centered in the
     * middle of the display.
     */
    public void messageBox(String pMessage) {
        JOptionPane.showMessageDialog(this, pMessage, "Message", JOptionPane.PLAIN_MESSAGE);
    }

}

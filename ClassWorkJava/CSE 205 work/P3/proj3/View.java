//**************************************************************************************************
// CLASS: View
//
// DESCRIPTION
// GUI class for the program
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

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.BoxLayout;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JTextField;

/**
 * The View class implements the GUI. It is a subclass of JFrame and implements the ActionListener
 * interface so that we can respond to user-initiated GUI events.
 */
public class View extends JFrame implements ActionListener {

    /**
     * The width of the View frame.
     */
    public static int FRAME_WIDTH = 525;

    /**
     * The height of the View frame.
     */
    public static int FRAME_HEIGHT = 225;
    
    /**
     * When the View() ctor is called from Main.run() to create the View, run() passes a reference
     * to the Main object as the argument to View(). We save that reference into mMain and then
     * later we can use mMain to communicate with the Main class.
     *
     * mMain is made accessible within this class via accessor/mutator methods getMain() and
     * setMain(). It shall not be directly accessed.
     */
    private Main3 mMain;

    /*
     * Declare GUI related instance variables for the buttons and text fields.
     */
    private JButton mClearButton;
    private JTextField[] mExamText;
    private JButton mExitButton;
    private JTextField[] mHomeworkText;
    private JButton mSaveButton;
    private JButton mSearchButton;
    private JTextField mStudentName;
    
    /**
     * View()
     *
     * The View constructor creates the GUI interface and makes the frame visible at the end.
     *
     * @param pMain is an instance of the Main class. This links the View to the Main class so
     * they may communicate with each other.
     */
    public View(Main3 pMain) {

        /**
         * Save a reference to the Main object pMain into instance var mMain by calling setMain().
         */
        setMain(pMain);
        
        /**
         * Create all GU elements and arrange them.
         */
        JPanel panelSearch = new JPanel();
        panelSearch.add(new JLabel("Student Name: "));
        mStudentName = new JTextField(25);
        panelSearch.add(mStudentName);
        mSearchButton = new JButton("Search");
        mSearchButton.addActionListener(this);
        panelSearch.add(mSearchButton);
        
        JPanel panelHomework = new JPanel();
        panelHomework.add(new JLabel("Homework: "));
        mHomeworkText = new JTextField[mMain.getNumHomeworks()];
        for (int i = 0; i < mHomeworkText.length; i++) {
            mHomeworkText[i] = new JTextField(5);
            panelHomework.add(mHomeworkText[i]);
        }
        
        JPanel panelExam = new JPanel();
        panelExam.add(new JLabel("Exam: "));
        mExamText = new JTextField[mMain.getNumExams()];
        for (int i = 0; i < mExamText.length; i++) {
            mExamText[i] = new JTextField(5);
            panelExam.add(mExamText[i]);
        }

        JPanel panelButtons = new JPanel();
        mClearButton = new JButton("Clear");
        mSaveButton = new JButton("Save");
        mExitButton = new JButton("Exit");
        mClearButton.addActionListener(this);
        mSaveButton.addActionListener(this);
        mExitButton.addActionListener(this);
        panelButtons.add(mClearButton);
        panelButtons.add(mSaveButton);
        panelButtons.add(mExitButton);
        
        JPanel panelMain = new JPanel();
        panelMain.setLayout(new BoxLayout(panelMain, BoxLayout.Y_AXIS));
        panelMain.add(panelSearch);
        panelMain.add(panelHomework);
        panelMain.add(panelExam);
        panelMain.add(panelButtons);
        
        // Set the title of the View to whatever you want by calling setTitle()
        setTitle("Gred :: Gradebook Editor");
        
        // Set the size of the View to FRAME_WIDTH x FRAME_HEIGHT
        setSize(FRAME_WIDTH, FRAME_HEIGHT);
        
        // Make the View non-resizable
        setResizable(false);
        
        // Set the default close operation to JFrame.DO_NOTHING_ON_CLOSE. This disables the X close
        // button in the title bar of the View so now the only way to exit the program is by click-
        // ing the Exit button. This ensures that Main.exit() will be called so it will write the
        // student records back out to the gradebook database.
        setDefaultCloseOperation(JFrame.DO_NOTHING_ON_CLOSE);
        
        // Add panelMain to the View.
        add(panelMain);

        // Now display the View by calling setVisible().
        setVisible(true);
    }

    /**
     * actionPerformed()
     *
     * Called when one of the JButtons is clicked. Detects which button was clicked and handles it.
     *
     * Make sure to write the @Override annotation to prevent accidental overloading because we are
     * overriding JFrame.actionPerformed().
     *
     *@param pEvent the triggering event
     */
    @Override
    public void actionPerformed(ActionEvent pEvent) {
        if (pEvent.getActionCommand().equals("Search")) {
            clearNumbers();
            String lastName = mStudentName.getText();
            if (lastName.equals("")) {
                messageBox("Please enter the student's last name.");
            }
            else {
                Student.setCurrStudent(getMain().search(lastName));
                if (Student.getCurrStudent() == null) {
                    messageBox("Student not found. Try again.");
                    mStudentName.setText("");
                }
                else {
                    displayStudent(Student.getCurrStudent());
                }
            }
        }
        else if ((pEvent.getActionCommand().equals("Save"))) {
            if (Student.getCurrStudent() != null) {
                saveStudent(Student.getCurrStudent());
            }
        }
        else if ((pEvent.getActionCommand().equals("Clear"))) {
            clear();
        }
        else if ((pEvent.getActionCommand().equals("Exit"))) {
            if (Student.getCurrStudent() != null) {
                saveStudent(Student.getCurrStudent());
            }
                getMain().exit();
        }
    }

    /**
     * clear()
     *
     * Called when the Clear button is clicked. Clears all of the text fields by setting the
     * contents of each to the empty string.
     *
     * After clear() returns, no student information is being edited or displayed and mStudent
     * has been set to null.
     */
    private void clear() {
        mStudentName.setText("");
        clearNumbers();
        Student.setCurrStudent(null);
    }

    /**
     * clearNumbers()
     *
     * Clears the homework and exam fields.
     */
    private void clearNumbers() {
        for (int i = 0; i < mMain.getNumHomeworks(); i++) {
            mHomeworkText[i].setText("");
        }
        for (int i = 0; i < mMain.getNumExams(); i++) {
            mExamText[i].setText("");
        }
    }
    
    /**
     * displayStudent()
     *
     * Displays the full name of a student and the homework and exam scores for a student in the mHomeworkText and mExamText text
     * fields.
     *
     * @param pStudent is the Student who's scores we are going to use to populate the text fields.
     */
    private void displayStudent(Student pStudent) {
        mStudentName.setText(pStudent.getFullName());
        for (int i = 0; i < mMain.getNumHomeworks(); i++) {
            int hw = pStudent.getHomework(i);
            String hwstr = hw + "";
            mHomeworkText[i].setText(hwstr);
        }
        for (int i = 0; i < mMain.getNumExams(); i++) {
            int ex = pStudent.getExam(i);
            String exstr = ex + "";
            mExamText[i].setText(exstr);
        }
    }

    /**
     * Accessor method for mMain.
     * @return reference to main
     */ 
    private Main3 getMain() {
        return mMain;
    }    

    /**
     * messageBox()
     *
     * Displays a message box containing some text. Note: read the Java 8 API page for JOptionPane
     * to see what the constructor arguments are to showMessageDialog(). You want to pass the 
     * appropriate "thing" for the first argument so your message dialog window will be centered
     * in the middle of the View frame. If your View frame is centered in the middle of your screen
     * then you did not pass the right "thing".
     * 
     * @param pMessage message text to display
     */
    public void messageBox(String pMessage) {
        JOptionPane.showMessageDialog(this, pMessage, "Message", JOptionPane.PLAIN_MESSAGE);
    }

    /**
     * saveStudent()
     *
     * Retrieves the homework and exam scores for pStudent from the text fields and writes the
     * results to the Student record in the Roster.
     * 
     * @param student to save info for
     */
    private void saveStudent(Student pStudent) {
        for (int i = 0; i < mMain.getNumHomeworks(); i++) {
            String hwstr = mHomeworkText[i].getText();
            int hw = Integer.parseInt(hwstr);
            pStudent.setHomework(i, hw);
        }
        for (int i = 0; i < mMain.getNumExams(); i++) {
            String exstr = mExamText[i].getText();
            int ex = Integer.parseInt(exstr);
            pStudent.setExam(i, ex);
        }
    }
    
    /**
     * Mutator method for mMain.
     * @param refrence to main
     */ 
    private void setMain(Main3 pMain) {
        mMain = pMain;
    }    

}

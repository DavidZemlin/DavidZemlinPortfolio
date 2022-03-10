//**************************************************************************************************
// CLASS: Main
//
// DESCRIPTION
// The Main class for Project 3.
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

import java.io.FileNotFoundException;
import javax.swing.JFrame;

/**
 * The Main class containing the main() and run() methods.
 */
public class Main3 {

    /*
     * The number of exams given in the course.
     */
    private final static int NUM_EXAMS = 3;

    /*
     * The number of homework assignments in the course.
     */
    private final static int NUM_HOMEWORKS = 5;
    
    /**
     * The Roster of students that is read from the input file "gradebook.dat".
     */
    private Roster mRoster;

    /**
     * A reference to the View object.
     */
    private View mView;

    /**
     * This is where execution starts. Instantiate a Main object and then call run().
     */
    public static void main(String[] pArgs) {
        new Main3().run();
    }

    /**
     * exit() is called when the Exit button in the View is clicked. When we exit we have to write
     * the roster to the output file "gradebook.dat". Then we exit the program with a code of 0.
     *
     * We open the file and write the roster to it in a try-catch block, where we catch a
     * FileNotFoundException that will be thrown if for some reason, we cannot open "gradebook.dat"
     * for writing.
     */ 
    public void exit() {
        try {
            GradebookWriter gbWriter = new GradebookWriter("D:\\OneDrive\\Zemlin Family Folder\\3rd.dat");//("gradebook.dat");
            gbWriter.writeGradebook(getRoster());
            gbWriter.close();
            System.exit(0);
        }
        catch (FileNotFoundException e) {
            getView().messageBox("Could not open gradebook.dat for writing. Exiting without saving.");
            System.exit(-1);
        }
    }

    /**
     * This method returns the number of exams in the class by returning the constant NUM_EXAMS.
     */
    public static final int getNumExams() {
        return NUM_EXAMS;
    }

    /**
     * This method returns the number of homework assignments in the class by returning the
     * constant NUM_HOMEWORKS.
     */
    public static final int getNumHomeworks() {
        return NUM_HOMEWORKS;
    }
    
    /**
     * Accessor method for mRoster.
     */
    private Roster getRoster() {
        return mRoster;
    }

    /**
     * Accessor method for mView.
     */
    private View getView() {
        return mView;
    }

    /**
     * run() is the main routine and is called from main().
     */
    private void run() {
        JFrame.setDefaultLookAndFeelDecorated(true);
        setView(new View(this));
        
        try {
            GradebookReader gbReader = new GradebookReader("gradebook.dat");
            setRoster(gbReader.readGradebook());
        }
        catch(FileNotFoundException e) {
            getView().messageBox("Could not open gradebook.dat for reading. Exiting.");
            System.exit(-1);
        }
    }

    /**
     * search() is called when the Search button is clicked in the View. The input parameter is
     * the last name of the Student to search the roster for. Call getStudent(pLastName) on the
     * Roster object (call getRoster() to get the reference to the Roster) to get a reference to
     * the Student with that last name. If the student is not located, getStudent() returns null.
     *
     * @param pLastName The last name of the student who we will search the Roster for.
     */
    public Student search(String pLastName) {
        return getRoster().getStudent(pLastName);
    }

    /**
     * Mutator method for mRoster.
     */
    private void setRoster(Roster pRoster) {
        mRoster = pRoster;
    }

    /**
     * Mutator method for mView.
     */
    private void setView(View pView) {
        mView = pView;
    }
}

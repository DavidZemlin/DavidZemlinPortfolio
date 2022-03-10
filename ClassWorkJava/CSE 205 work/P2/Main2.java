//**************************************************************************************************
// CLASS: Main
//
// DESCRIPTION
// The Main class for Project 2.
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
import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Scanner;

public class Main2 {

    /**
     * Instantiate a Main object and call run() on the object. Note that essentially now, run()
     * becomes the starting point of execution for the program.
     */
    public static void main(String[] pArgs) {
        new Main2().run();
    }

    /**
     *  Calls other methods to implement the sw requirements.
     */
    private void run() {
        ArrayList<Student> studentList = null;
        
        // attempt to read a txt file input of student information
        try {
            studentList = readFile();
        }
        catch (FileNotFoundException ex) {
            System.out.println("Sorry, could not open 'p02-students.txt' for reading. Stopping.");
            System.exit(-1);
        }
        
        // calculate the tuition of each student
        calcTuition(studentList);
        
        // Sort the students into ascending order
        Sorter.insertionSort(studentList, Sorter.SORT_ASCENDING);
        
        // write a file of all student tuition info
        
        try {
            writeFile(studentList);
        }
        catch(FileNotFoundException ex) {
            System.out.println("Sorry, could not open 'p02-tuition.txt' for writing. Stopping.");
            System.exit(-1);
        }
    }

    /**
     * Calculates the tuition for each Student in pStudentList. Write an enhanced for loop that
     * iterates over each Student in pStudentList. For each Student, call calcTuition() on that
     * Student object.
     */
    private void calcTuition(ArrayList<Student> pStudentList) {
        for (Student student : pStudentList) {
            student.calcTuition();
        }
    }

    /**
     * Reads the student information from "p02-students.txt" and returns the list of students as
     * an ArrayList<Student> object. Note that this method throws FileNotFoundException if the
     * input file could not be opened.
     */
    private ArrayList<Student> readFile() throws FileNotFoundException {
        ArrayList<Student> studentList = new ArrayList<>();
        Scanner in = new Scanner(new File("p02-students.txt"));
        while (in.hasNext() == true) {
            String studentType = in.next();
            if (studentType.equals("C")) {
                studentList.add(readOnCampusStudent(in));
            }
            else {
                studentList.add(readOnlineStudent(in));
            }
        }
        in.close();
        return studentList;
    }
    

    /**
     * Reads the information for an on-campus student from the input file.
     */
    private OnCampusStudent readOnCampusStudent(Scanner pIn) {
        String id = pIn.next();
        String lname = pIn.next();
        String fname = pIn.next();
        OnCampusStudent student = new OnCampusStudent(id, fname, lname);
        String res = pIn.next();
        double fee = pIn.nextDouble();
        int credits = pIn.nextInt();
        if (res.equals("R")) {
            student.setResidency(student.RESIDENT) ;
        }
        else {
            student.setResidency(student.NON_RESIDENT);
        }
        student.setProgramFee(fee);
        student.setCredits(credits);
        return student;
    }

    /**
     * Reads the information for an online student from the input file.
     */
    private OnlineStudent readOnlineStudent(Scanner pIn) {
        String id = pIn.next();
        String lname = pIn.next();
        String fname = pIn.next();
        OnlineStudent student = new OnlineStudent(id, fname, lname);
        String fee = pIn.next();
        int credits = pIn.nextInt();
        if (fee.equals("T")) {
            student.setTechFee(true);
        }
        else {
            student.setTechFee(false);
        }
        student.setCredits(credits);
        return student;
    }

    /**
     * Writes the output to "p02-tuition.txt" per the software requirements. Note that this method 
     * throws FileNotFoundException if the output file could not be opened.
     */
    private void writeFile(ArrayList<Student> pStudentList) throws FileNotFoundException {
        PrintWriter out = new PrintWriter(new File("p02-tuition.txt"));
        for (Student stu: pStudentList) {
            out.printf("%-16s%-20s%-15s%8.2f%n", stu.getId(), stu.getLastName(), stu.getFirstName(), stu.getTuition());
        }
        out.close();
    }

}

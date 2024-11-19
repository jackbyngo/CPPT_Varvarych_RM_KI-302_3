package KI302Varvarych.Lab4;



import java.util.Scanner;
import java.io.*;
import static java.lang.System.out;

/**
 * Class <code>EquationsApp</code> Implements driver for Equations class
 * @version 1.0
 */
public class EquationsApp {
    public static void main(String[] args) {
        out.print("Enter file name: ");
        Scanner in = new Scanner(System.in);
        String fName = in.nextLine();
        PrintWriter fout = null;

        try {
            fout = new PrintWriter(new File(fName));
            Equations eq = new Equations();
            out.print("Enter X: ");
            int x = in.nextInt();

            double result = eq.calculate(x);

            fout.print(result);
            fout.flush();
            fout.close();
        } catch (Exception ex) {
            if (ex instanceof FileNotFoundException) {
                out.print("Exception reason: File not found");
            } else if (ex instanceof CalcException) {
                out.print(ex.getMessage());
            } else {
                out.print("Unknown exception occurred");
            }
        }
        finally{
            if (fout != null) {
                fout.close();
            }
        }
    }
}
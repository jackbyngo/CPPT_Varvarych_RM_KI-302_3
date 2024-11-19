package KI302Varvarych.Lab5;

import java.io.*;
import java.util.Scanner;

/**
 * Клас <code>EquationsAppTest</code> тестує методи класу <code>EquationsIO</code>.
 * */
public class EquationsApp {
    public static void main(String[] args) throws IOException {
        Equations eq = new Equations();
        EquationsIO eqIO = new EquationsIO();

        Scanner s = new Scanner(System.in);
        System.out.print("Enter X (in degrees): ");
        int x = s.nextInt();
        double result = eq.calculate(x);
        System.out.println("Result is: " + result);

        eqIO.setResult(result);
        eqIO.writeResTxt("textRes.txt");

        eqIO.writeResBin("BinRes.bin");

        eqIO.readResBin("BinRes.bin");
        System.out.println("Result from binary file is: " + eqIO.getResult());

        eqIO.readResTxt("textRes.txt");
        System.out.println("Result from text file is: " + eqIO.getResult());

    }
}

package temp;

import java.io.*;
import java.util.*;
import static java.nio.charset.StandardCharsets.UTF_8;

/**
 * Клас Lab1VarvarychKI302 реалізує лабораторну роботу №1
 * */
//dddddddddddddddddddddddddddddddddddddddddddddddddddddddd

public class Lab1VarvarychKI302 {
    /**
     * Статичний метод main є точкою входу в програму
     *
     * @param args аргументи
     * */

    public static void main(String[] args) throws FileNotFoundException {
        PrintStream out = new PrintStream(System.out, true, UTF_8);
        out.println("Введіть розмір квадратної матриці: ");
        Scanner in = new Scanner(System.in);
        int nRows = in.nextInt();
        out.println("Введіть символ-заповнювач: ");
        in.nextLine();
        String filler = in.nextLine();
        if (filler.length() != 1)
        {
            out.print("\nСимвол-заповнювач введено невірно.");
            System.exit(0);
        }
        char[][] arr = new char[nRows][];

        for (int I = 0; I < nRows; I++)
        {
            for (int K = 0; K < I; K++)
                out.print("\t");
            arr[I] = new char[nRows - I];
            for (int J = 0; J< nRows - I; J++)
            {
                if(J>=I ){
                if(J==I){
                    arr[I][J]='*';
                }
                else {
                    arr[I][J] = (char) filler.codePointAt(0);

                }
                    out.print(arr[I][J] + "\t");
            }}


            out.println();
        }
        in.close();
        out.close();
    }
}

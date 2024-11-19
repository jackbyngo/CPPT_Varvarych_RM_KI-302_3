package KI302Varvarych.Lab5;

import java.io.*;
import java.util.Scanner;

/**
 * Клас <code>EquationsIO</code> реалізує методи читання та запису
 */
public class EquationsIO {
    private double result;

    /**
     * Зберігає результат у текстовому форматі
     */
    public void writeResTxt(String fName) throws FileNotFoundException {
        PrintWriter f = new PrintWriter(fName);
        f.printf("%f ", result);
        f.close();
    }

    /**
     * Читає результат із текстового файлу
     */
    public void readResTxt(String fName) throws FileNotFoundException {
        File f = new File(fName);
        if (f.exists()) {
            Scanner s = new Scanner(f);
            result = s.nextDouble();
            s.close();
        } else {
            throw new FileNotFoundException("File " + fName + " not found");
        }
    }

    /**
     * Зберігає результат у двійковому форматі
     */
    public void writeResBin(String fName) throws IOException {
        DataOutputStream f = new DataOutputStream(new FileOutputStream(fName));
        f.writeDouble(result);
        f.close();
    }

    /**
     * Читає результат із двійкового файлу.
     */
    public void readResBin(String fName) throws IOException {
        DataInputStream f = new DataInputStream(new FileInputStream(fName));
        result = f.readDouble();
        f.close();
    }

    /**
     * Задає результат обчислень
     */
    public void setResult(double res) {
        this.result = res;
    }

    /**
     * Повертає результат обчислень.
     */
    public double getResult() {
        return result;
    }
}

package KI302Varvarych.Lab3;

import java.io.FileNotFoundException;

public class DogApp {
    public static void main(String[] args) throws FileNotFoundException {

            ExperimentalDog expDog = new ExperimentalDog(100,"meat",20,15);
            expDog.bark();
            expDog.eat();
            expDog.run();
            expDog.sleep();
            expDog.conductExperiment();
            expDog.specialAction();
            expDog.showResults();
            expDog.dispose();

    }
}

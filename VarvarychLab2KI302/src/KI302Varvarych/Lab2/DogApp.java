package KI302Varvarych.Lab2;

import java.io.FileNotFoundException;

public class DogApp {
    public static void main(String[] args) throws FileNotFoundException {
        Dog dog = new Dog();

        dog.fout.print("Initial energy level: " + dog.getEnergyLevel() + "\n");
        dog.fout.flush();

        dog.bark();

        dog.fout.print("Dog's food type: " + dog.getFoodType() + "\n");
        dog.fout.print(("Food energy "+ dog.getFoodEnergy()+"\n"));

        dog.eat();

        dog.setRunEnergy(20);
        dog.fout.print("Energy is needed for running "+dog.getRunEnergy()+"\n");
        dog.run();

        dog.sleep();


        dog.setFood("Meat", 20);
        dog.fout.print("Updated food type: " + dog.getFoodType() + " with energy: " + dog.getFoodEnergy() + "\n");
        dog.fout.flush();

        dog.fout.print(("Food energy "+ dog.getFoodEnergy()+"\n"));

        dog.eat();

        dog.fout.print("Energy level after eat: " + dog.getEnergyLevel() + "\n");
        dog.fout.flush();

        dog.dispose();
    }
}

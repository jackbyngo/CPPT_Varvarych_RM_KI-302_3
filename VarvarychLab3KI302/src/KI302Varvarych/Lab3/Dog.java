package KI302Varvarych.Lab3;

import java.io.*;

/**
 * Клас Dog представляє собаку, яка може їсти, бігати, спати і гавкати
 * Клас також записує події в файл.
 */
public abstract class Dog {
    private Energy energy;
    private Food food;
    private Run run;
    public PrintWriter fout;

    /**
     * Конструктор за замовчуванням створює собаку з початковою енергією, їжею та рівнем енергії
     */
    public Dog() throws FileNotFoundException {
        this.energy = new Energy(100);
        this.food = new Food("Корм", 5);
        this.run = new Run(5);
        fout = new PrintWriter(new File("Log.txt"));
    }

    /**
     * Конструктор з параметрами для створення собаки
     * @param energyLevel початковий рівень енергії собаки
     * @param foodType тип їжі
     * @param foodEnergy кількість енергії, яку дає їжа
     * @param runEnergy кількість енергії, яку витрачає собака під час бігу
     * @throws FileNotFoundException якщо файл журналу не може бути створений
     */
    public Dog(int energyLevel, String foodType, int foodEnergy, int runEnergy) throws FileNotFoundException {
        this.energy = new Energy(energyLevel);
        this.food = new Food(foodType, foodEnergy);
        this.run = new Run(runEnergy);
        fout = new PrintWriter(new File("Log.txt"));
    }

    /**
     * Собака гавкає
     */
    public void bark() {
        fout.print("Dog barking\n");
        fout.flush();
    }

    /**
     * Собака їсть і збільшує свою енергію
     */
    public void eat() {
        energy.increaseEnergy(food.getEnergy());
        fout.print("Dog eating " + food.getType() + " and energy is now " + energy.getEnergy() + "\n");
        fout.flush();
    }

    /**
     * Собака бігає і зменшує свою енергію
     */
    public void run() {
        energy.decreaseEnergy(run.getEnergy());
        fout.print("Dog running and energy is now " + energy.getEnergy() + "\n");
        fout.flush();
    }

    /**
     * Собака спить
     */
    public void sleep() {
        fout.print("Dog is sleeping\n");
        fout.flush();
    }

    /**
     * Встановлює новий тип їжі і її значення
     * @param type тип їжі
     * @param energy кількість енергії, яку дає їжа
     */
    public void setFood(String type, int energy) {
        this.food.setType(type);
        this.food.setEnergy(energy);
    }

    /**
     * Повертає тип їжі
     */
    public String getFoodType() {
        return food.getType();
    }

    /**
     * Повертає кількість енергії, яку дає їжа
     */
    public int getFoodEnergy() {
        return food.getEnergy();
    }

    /**
     * Встановлює кількість енергії, яку собака витрачає під час бігу
     */
    public void setRunEnergy(int energy) {
        this.run.setEnergy(energy);
    }

    /**
     * Повертає кількість енергії, яку собака витрачає під час бігу
     */
    public int getRunEnergy() {
        return run.getEnergy();
    }

    /**
     * Повертає поточний рівень енергії собаки.
     */
    public int getEnergyLevel() {
        return energy.getEnergy();
    }

    /**
     * Закриває файл журналу.
     */
    public void dispose() {
        fout.close();
    }



}
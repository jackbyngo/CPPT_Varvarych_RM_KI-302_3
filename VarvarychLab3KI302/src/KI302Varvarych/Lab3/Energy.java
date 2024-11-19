package KI302Varvarych.Lab3;

/**
 * Клас Energy представляє енергію собаки.
 */
public  class Energy {
    private int energy;

    /**
     * Конструктор створює об'єкт з визначеною кількістю енергії.
     */
    public Energy(int energy) {
        this.energy = energy;
    }

    /**
     * Збільшує рівень енергії.
     */
    public void increaseEnergy(int amount) {
        this.energy += amount;
    }

    /**
     * Зменшує рівень енергії.
     */
    public void decreaseEnergy(int amount) {
        this.energy -= amount;
    }

    /**
     * Повертає поточний рівень енергії.
     */
    public int getEnergy() {
        return energy;
    }
}






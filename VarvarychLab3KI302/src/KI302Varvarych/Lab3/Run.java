package KI302Varvarych.Lab3;

/**
 * Клас Run представляє активність бігу, яка зменшує енергію собаки.
 */
public  class Run {
    private int energy;

    /**
     * Конструктор створює активність бігу з визначеним рівнем енергії.
     */
    public Run(int energy) {
        this.energy = energy;
    }

    /**
     * Встановлює кількість енергії, яку собака витрачає під час бігу.
     */
    public void setEnergy(int energy) {
        this.energy = energy;
    }

    /**
     * Повертає кількість енергії, яку собака витрачає під час бігу.
     */
    public int getEnergy() {
        return energy;
    }
}

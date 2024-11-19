package KI302Varvarych.Lab3;

/**
 * Клас Food представляє їжу, яку їсть собака.
 */
public class Food {
    private String type;
    private int energy;

    /**
     * Конструктор створює їжу з визначеним типом і кількістю енергії.
     */
    public Food(String type, int energy) {
        this.type = type;
        this.energy = energy;
    }

    /**
     * Встановлює тип їжі.
     */
    public void setType(String type) {
        this.type = type;
    }

    /**
     * Повертає тип їжі.
     */
    public String getType() {
        return type;
    }

    /**
     * Встановлює кількість енергії, яку дає їжа.
     */
    public void setEnergy(int energy) {
        this.energy = energy;
    }

    /**
     * Повертає кількість енергії, яку дає їжа.
     */
    public int getEnergy() {
        return energy;
    }
}




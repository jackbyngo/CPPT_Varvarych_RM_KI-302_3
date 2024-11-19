    package KI302Varvarych.Lab3;

    import java.io.*;

    /**
     * Клас, що представляє піддослідного пса.
     * Успадковує абстрактний клас Dog та реалізує інтерфейс Experimentable.
     */
    public class ExperimentalDog extends Dog implements Experimentable {

        /**
         * Конструктор за замовчуванням для створення піддослідного пса.
         */
        public ExperimentalDog() throws FileNotFoundException {
            super();
        }

        /**
         * Конструктор із параметрами для створення піддослідного пса.
         * @param energyLevel початковий рівень енергії
         * @param foodType тип їжі
         * @param foodEnergy енергія від їжі
         * @param runEnergy енергія, що витрачається під час бігу
         * @throws FileNotFoundException якщо не вдається створити файл журналу
         */
        public ExperimentalDog(int energyLevel, String foodType, int foodEnergy, int runEnergy) throws FileNotFoundException {
            super(energyLevel, foodType, foodEnergy, runEnergy);
        }

        /**
         * Проведення експерименту.
         */
        @Override
        public void conductExperiment() {
            fout.print("Пес бере участь в експерименті.\n");
            fout.flush();
        }

        /**
         * Спеціальна дія під час експерименту.
         */
        @Override
        public void specialAction() {
            fout.print("Пес виконує спеціальну дію під час експерименту.\n");
            fout.flush();
        }


        /**
         * Показ результатів експерименту.
         */
        @Override
        public void showResults() {
            fout.print("Експеримент завершено. Результати успішні!\n");
            fout.flush();
        }

    }

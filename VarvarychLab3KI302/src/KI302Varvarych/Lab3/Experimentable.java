package KI302Varvarych.Lab3;

/**
 * Інтерфейс, що визначає поведінку для проведення експериментів.
 * Описує основні дії, які може виконувати піддослідний об'єкт.
 */
public interface Experimentable {

    /**
     * Метод для проведення експерименту.
     */
    void conductExperiment();

    /**
     * Спеціальна дія під час експерименту.
     */
    void specialAction();

    /**
     * Метод для відображення результатів експерименту.
     */
    void showResults();
}

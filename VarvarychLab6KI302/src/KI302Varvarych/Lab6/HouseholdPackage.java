package KI302Varvarych.Lab6;

import java.util.ArrayList;


/**
 * Параметризований клас-контейнер для побутових предметів.
 *
 * @param <T> тип об'єктів, які можуть бути додані до контейнера, має бути підкласом HouseholdItem
 */
class HouseholdPackage<T extends HouseholdItem> {
    private final ArrayList<T> items;

    /**
     * Конструктор, який створює новий порожній контейнер.
     */
    public HouseholdPackage() {
        items = new ArrayList<>();
    }

    /**
     * Знаходить і повертає найбільший предмет у контейнері.

     */
    public T findMax() {
        if (!items.isEmpty()) {
            T maxItem = items.get(0);
            for (int i = 1; i < items.size(); i++) {
                if (items.get(i).compareTo(maxItem) > 0) {
                    maxItem = items.get(i);
                }
            }
            return maxItem;
        }
        return null;
    }

    /**
     * Додає новий предмет до контейнера.
     */
    public void addItem(T item) {
        items.add(item);
        System.out.print("Element added: ");
        item.print();
    }

    /**
     * Видаляє предмет з контейнера за індексом.

     */
    public void removeItem(int index) {
        if (index >= 0 && index < items.size()) {
            items.remove(index);
        }
    }

    /**
     * Виводить інформацію про всі предмети, які знаходяться в контейнері.
     */
    public void displayAllItems() {
        for (T item : items) {
            item.print();
        }
    }
    public String displayItemCounts() {
        int cleaningProductCount = 0;
        int foodProductCount = 0;

        for (T item : items) {
            if (item instanceof CleaningProduct) {
                cleaningProductCount++;
            } else if (item instanceof FoodProduct) {
                foodProductCount++;
            }
        }

        return "\nCleaning Products: " + cleaningProductCount +
                "\nFood Products:" + foodProductCount;
    }
}




/**
 * Інтерфейс для побутових предметів.
 */
interface HouseholdItem extends Comparable<HouseholdItem> {
    public int getWeight();
    public void print();
}

/**
 * Клас засобів для прибирання.
 * Реалізує інтерфейс HouseholdItem, що дозволяє порівнювати предмети за вагою.
 */
class CleaningProduct implements HouseholdItem {
    private String name;
    private int weight;

    /**
     * Конструктор, який створює новий засіб для прибирання.
     */
    public CleaningProduct(String name, int weight) {
        this.name = name;
        this.weight = weight;
    }

    /**
     * Повертає назву засобу для прибирання.
     */
    public String getName() {
        return name;
    }

    /**
     * Повертає вагу засобу для прибирання.
     */
    public int getWeight() {
        return weight;
    }

    /**
     * Порівнює два предмети побуту за вагою.
     */
    public int compareTo(HouseholdItem other) {
        Integer thisWeight = this.weight;
        return thisWeight.compareTo(other.getWeight());
    }

    /**
     * Виводить інформацію про засіб для прибирання.
     */
    public void print() {
        System.out.println("Cleaning Product: " + name + ", Weight: " + weight + " g");
    }
}

/**
 * Клас продуктів харчування.
 * Реалізує інтерфейс HouseholdItem, що дозволяє порівнювати продукти за вагою.
 */
class FoodProduct implements HouseholdItem {
    private String name;
    private int weight;

    /**
     * Конструктор, який створює новий продукт харчування.
     */
    public FoodProduct(String name, int weight) {
        this.name = name;
        this.weight = weight;
    }

    /**
     * Повертає назву продукту.
     */
    public String getName() {
        return name;
    }



    /**
     * Повертає вагу продукту.
     */
    public int getWeight() {
        return weight;
    }

    /**
     * Порівнює два продукти за вагою
     */
    public int compareTo(HouseholdItem other) {
        Integer thisWeight = this.weight;
        return thisWeight.compareTo(other.getWeight());
    }

    /**
     * Виводить інформацію про продукт харчування
     */
    public void print() {
        System.out.println("Food Product: " + name + ", Weight: " + weight + " g");
    }
}

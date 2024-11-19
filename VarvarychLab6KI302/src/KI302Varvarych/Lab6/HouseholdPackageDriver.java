package KI302Varvarych.Lab6;

public class HouseholdPackageDriver {
    public static void main(String[] args) {

        HouseholdPackage<? super HouseholdItem> packageContainer = new HouseholdPackage<>();

        packageContainer.addItem(new CleaningProduct("Soap", 500));
        packageContainer.addItem(new FoodProduct("Apple", 120));
        packageContainer.addItem(new CleaningProduct("Glass Cleaner", 300));
        packageContainer.addItem(new FoodProduct("Milk", 1000));
        System.out.println(packageContainer.displayItemCounts());
        HouseholdItem maxItem = packageContainer.findMax();

        System.out.print("The heaviest item in the household package is: \n");
        if (maxItem != null) {
            maxItem.print();
        }

        System.out.println("\nAll items in the household package:");
        packageContainer.displayAllItems();
    }

}
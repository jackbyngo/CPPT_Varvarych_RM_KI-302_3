from dog import Dog
from police_dog import PoliceDog

if __name__ == "__main__":
    # Створюємо звичайну собаку
    dog1 = Dog("Buddy", "Labrador")
    print(f"{dog1.name} is a {dog1.breed}. {dog1.bark()}")
    print(dog1.play(2))  # Собака грає 2 години
    print(dog1.sleep(5))  # Собака спить 5 годин

    # Створюємо першу піддослідну собаку
    police_dog1 = PoliceDog("Rex", "German Shepherd", "K9 Unit")
    print(f"{police_dog1.name} is a police dog of {police_dog1.breed} breed.")
    print(police_dog1.bark())
    print(police_dog1.train(3))  # Поліцейська собака навчається 3 години

    # Створюємо другу піддослідну собаку, яка успішно виконує обов'язки
    police_dog2 = PoliceDog("Max", "Doberman", "K9 Unit")
    police_dog2.energy_level = 40  # Налаштовуємо рівень енергії
    police_dog2.training_level = 60  # Налаштовуємо рівень навчання
    print(f"{police_dog2.name} is a police dog of {police_dog2.breed} breed.")
    print(police_dog2.bark())
    print(police_dog2.perform_duty())  # Поліцейська собака успішно виконує обов'язок

from dog import Dog
from police_dog import PoliceDog

if __name__ == "__main__":
    # ��������� �������� ������
    dog1 = Dog("Buddy", "Labrador")
    print(f"{dog1.name} is a {dog1.breed}. {dog1.bark()}")
    print(dog1.play(2))  # ������ ��� 2 ������
    print(dog1.sleep(5))  # ������ ����� 5 �����

    # ��������� ����� ��������� ������
    police_dog1 = PoliceDog("Rex", "German Shepherd", "K9 Unit")
    print(f"{police_dog1.name} is a police dog of {police_dog1.breed} breed.")
    print(police_dog1.bark())
    print(police_dog1.train(3))  # ���������� ������ ��������� 3 ������

    # ��������� ����� ��������� ������, ��� ������ ������ ����'����
    police_dog2 = PoliceDog("Max", "Doberman", "K9 Unit")
    police_dog2.energy_level = 40  # ����������� ����� ����㳿
    police_dog2.training_level = 60  # ����������� ����� ��������
    print(f"{police_dog2.name} is a police dog of {police_dog2.breed} breed.")
    print(police_dog2.bark())
    print(police_dog2.perform_duty())  # ���������� ������ ������ ������ ����'����

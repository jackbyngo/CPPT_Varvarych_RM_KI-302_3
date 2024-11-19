from dog import Dog

class PoliceDog(Dog):
    def __init__(self, name, breed, department):
        """
        Initialize a police dog with a name, breed, and department.
        Inherits from the Dog class.
        """
        super().__init__(name, breed)
        self.department = department
        self.training_level = 0

    def train(self, hours):
        """Simulate police dog training and increase training level."""
        self.training_level += hours * 3
        return f"{self.name} trained for {hours} hours. Training level: {self.training_level}"

    def perform_duty(self):
        """Simulate the police dog performing duty and decrease energy level."""
        if self.energy_level >= 30 and self.training_level >= 50:
            self.energy_level -= 30
            self.training_level -= 50
            return f"{self.name} successfully performed duty. Energy level: {self.energy_level}. Training level: {self.training_level}"
        else:
            return f"{self.name} is not ready to perform duty."



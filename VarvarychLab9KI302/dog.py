class Dog:
    def __init__(self, name, breed):
        """Initialize a dog with a name and breed."""
        self.name = name
        self.breed = breed
        self.energy_level = 100

    def bark(self):
        """Return the sound a dog makes."""
        return "Woof!"

    def play(self, hours):
        """Simulate the dog playing and decrease energy level."""
        energy_consumed = hours * 10
        if self.energy_level >= energy_consumed:
            self.energy_level -= energy_consumed
            return f"{self.name} played for {hours} hours. Energy level: {self.energy_level}"
        else:
            return f"{self.name} is too tired to play."

    def sleep(self, hours):
        """Simulate the dog sleeping and increase energy level."""
        self.energy_level += hours * 5
        return f"{self.name} slept for {hours} hours. Energy level: {self.energy_level}"
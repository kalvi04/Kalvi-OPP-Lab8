
using System;
using AnimalLibrary;
using FractionLibrary;

namespace AnimalLibrary
{
    public class Animal
    {
        public int Age { get; set; }
        public double Weight { get; set; }
        public string Gender { get; set; }

        public Animal() { }

        public Animal(int age, double weight, string gender)
        {
            Age = age;
            Weight = weight;
            Gender = gender;
        }

        public Animal(Animal other)
        {
            Age = other.Age;
            Weight = other.Weight;
            Gender = other.Gender;
        }

        public void ChangeAge(int newAge) => Age = newAge;

        public void ChangeWeight(double newWeight) => Weight = newWeight;

        public virtual string ShowInfo() => $"Animal [Age: {Age}, Weight: {Weight}, Gender: {Gender}]";
    }

    public class Fish : Animal
    {
        public string WaterType { get; set; } 

        public Fish() { }

        public Fish(int age, double weight, string gender, string waterType)
            : base(age, weight, gender)
        {
            WaterType = waterType;
        }

        public Fish(Fish other) : base(other)
        {
            WaterType = other.WaterType;
        }

        public void ChangeWaterType(string newType) => WaterType = newType;

        public override string ShowInfo() => base.ShowInfo() + $", WaterType: {WaterType}";
    }

    public class Bird : Animal
    {
        public bool CanFly { get; set; }

        public Bird() { }

        public Bird(int age, double weight, string gender, bool canFly)
            : base(age, weight, gender)
        {
            CanFly = canFly;
        }

        public Bird(Bird other) : base(other)
        {
            CanFly = other.CanFly;
        }

        public void ChangeFlightAbility(bool canFly) => CanFly = canFly;

        public override string ShowInfo() => base.ShowInfo() + $", CanFly: {CanFly}";
    }
}

namespace FractionLibrary
{
    public class Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new DivideByZeroException("Denominator cannot be zero.");

            Numerator = numerator;
            Denominator = denominator;
            Simplify();
        }

        public Fraction(Fraction other)
        {
            Numerator = other.Numerator;
            Denominator = other.Denominator;
        }

        public static Fraction operator +(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Fraction operator -(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator - b.Numerator * a.Denominator, a.Denominator * b.Denominator);

        public static Fraction operator *(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Numerator, a.Denominator * b.Denominator);

        public static Fraction operator /(Fraction a, Fraction b) =>
            new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator);

        public static bool operator ==(Fraction a, Fraction b) =>
            a.Numerator * b.Denominator == b.Numerator * a.Denominator;

        public static bool operator !=(Fraction a, Fraction b) => !(a == b);

        public static bool operator >(Fraction a, Fraction b) =>
            a.Numerator * b.Denominator > b.Numerator * a.Denominator;

        public static bool operator <(Fraction a, Fraction b) =>
            a.Numerator * b.Denominator < b.Numerator * a.Denominator;

        public static bool operator >=(Fraction a, Fraction b) =>
            a.Numerator * b.Denominator >= b.Numerator * a.Denominator;

        public static bool operator <=(Fraction a, Fraction b) =>
            a.Numerator * b.Denominator <= b.Numerator * a.Denominator;

        public static explicit operator double(Fraction a) =>
            (double)a.Numerator / a.Denominator;

        public override string ToString() => $"{Numerator}/{Denominator}";

        public void Simplify()
        {
            int gcd = GCD(Numerator, Denominator);
            Numerator /= gcd;
            Denominator /= gcd;
        }

        private static int GCD(int a, int b) => b == 0 ? a : GCD(b, a % b);

        public override bool Equals(object obj) =>
            obj is Fraction fraction && this == fraction;

        public override int GetHashCode() => HashCode.Combine(Numerator, Denominator);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Select Task:");
        Console.WriteLine("1. Animal Classes");
        Console.WriteLine("2. Fraction Operations");

        int choice = int.Parse(Console.ReadLine() ?? "0");

        switch (choice)
        {
            case 1:
                DemoAnimalClasses();
                break;
            case 2:
                DemoFractionOperations();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }

    static void DemoAnimalClasses()
    {
        Animal[] animals = new Animal[3];
        animals[0] = new Fish(2, 3.5, "Male", "Oceanic");
        animals[1] = new Bird(1, 1.2, "Female", true);
        animals[2] = new Fish(5, 8.0, "Female", "Freshwater");

        foreach (var animal in animals)
        {
            Console.WriteLine(animal.ShowInfo());
        }
    }

    static void DemoFractionOperations()
    {
        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(2, 3);

        Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
        Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
        Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
        Console.WriteLine($"{f1} / {f2} = {f1 / f2}");
        Console.WriteLine($"{f1} == {f2}: {f1 == f2}");
        Console.WriteLine($"{f1} > {f2}: {f1 > f2}");
        Console.WriteLine($"{f1} as double: {(double)f1}");
    }
}
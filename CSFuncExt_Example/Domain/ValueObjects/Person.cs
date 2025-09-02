using CSharpFunctionalExtensions;

namespace CSFuncExt_Example.Domain.ValueObjects;

public class Person : ValueObject
{
    public static class Errors
    {
        public const string NameEmpty = "Name cannot be empty";
        public const string LastNameEmpty = "Last name cannot be empty";
        public const string AgeNegative = "Age cannot be negative";
    }
    
    private Person(string name, string lastName, int age)
    {
        Name = name;
        LastName = lastName;
        Age = age;
    }

    public string Name { get; }
    public string LastName { get; }
    public int Age { get; }

    public static Result<Person> Create(string name, string lastName, int age)
    {
        return Result.Success()
            .Ensure(() => !string.IsNullOrWhiteSpace(name), Errors.NameEmpty)
            .Ensure(() => !string.IsNullOrWhiteSpace(lastName), Errors.LastNameEmpty)
            .Ensure(() => age is >= 0 and < 101, Errors.AgeNegative)
            .Map(() => new Person(name, lastName, age));
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return LastName;
        yield return Age;
    }
}
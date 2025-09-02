using CSFuncExt_Example.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentAssertions;

namespace TestProject1;

public class PersonTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EqualityTestPositive()
    {
        var person1 = Person.Create("John", "Doe", 30);
        var person2 = Person.Create("John", "Doe", 30);

        Result.Combine(person1, person2).IsSuccess.Should().BeTrue();
        Assert.That(person1.Value == person2.Value, Is.True);
    }
    
    [Test]
    public void EqualityTestNegative()
    {
        var person1 = Person.Create("John", "Doe", 30);
        var person2 = Person.Create("Johns", "Doe", 30);

        Result.Combine(person1, person2).IsSuccess.Should().BeTrue();
        Assert.That(person1.Value == person2.Value, Is.False);
    }
    
    [Test]
    [TestCase("John", "", 30, Person.Errors.LastNameEmpty)]
    [TestCase("", "Doe", 30, Person.Errors.NameEmpty)]
    [TestCase("John", "Doe", 101, Person.Errors.AgeNegative)]
    public void CreatePerson_Validations(string name, string lastName, int age, string expectedError)
    {
        var result = Person.Create(name, lastName, age);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(expectedError);
    }
}
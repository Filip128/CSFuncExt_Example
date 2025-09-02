using CSFuncExt_Example.Domain;
using CSFuncExt_Example.Domain.ValueObjects;
using FluentAssertions;

namespace TestProject1;

public class RoomTests
{
    [Test]
    public void CreateToomTests()
    {
        Room.Create("Room1").IsSuccess.Should().BeTrue();
        Room.Create("").IsSuccess.Should().BeFalse();
    }
    
    [Test]
    public void AddPersonTests()
    {
        var room = Room.Create("Room1").Value;
        var person1 = Person.Create("John", "Doe", 30).Value;
        var person2 = Person.Create("Jane", "Doe", 25).Value;
        var person3 = Person.Create("John", "Smith", 40).Value;

        room.AddPerson(person1).IsSuccess.Should().BeTrue();
        room.AddPerson(person2).IsSuccess.Should().BeTrue();
        room.AddPerson(person3).IsSuccess.Should().BeFalse(); // Duplicate name
        room.AddPerson(person3).Error.Should().Be(Room.Errors.PersonAlreadyExists); // Duplicate name

        room.Persons.Count.Should().Be(2);
    }
}
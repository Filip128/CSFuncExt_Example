using CSFuncExt_Example.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace CSFuncExt_Example.Domain;

public class Room : IAggregateRoot
{
    public static class Errors
    {
        public const string RommNotNull = "Room name cannot be empty";
        public const string PersonAlreadyExists = "Person with the same name already exists in the room";
    }


    public int Id { get; private set; }
    public string Name { get; set; }
    private IList<Person> _person { get; set; } = new List<Person>();
    public IReadOnlyList<Person> Persons => _person.AsReadOnly();

    private Room(string name)
    {
        Name = name;
    }

    private Maybe<Person> GetFirstNameWithName(string name) => Persons.FirstOrDefault(p => p.Name == name);

    public Result<Person> EnsureFirstNameWithNae(string name) =>
        GetFirstNameWithName(name).ToResult($"No person with name {name} found in room {Name}");

    public Result AddPerson(Person person)
    {
        return GetFirstNameWithName(person.Name).ToInvertedResult(Errors.PersonAlreadyExists)
            .Tap(() => _person.Add(person));
    }


    public static Result<Room> Create(string name)
    {
        return Result.SuccessIf(!string.IsNullOrEmpty(name), Errors.RommNotNull)
            .Map(() => new Room(name));
    }
}
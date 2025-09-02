using CSharpFunctionalExtensions;

public class Person
{
    public string Imie { get; }
    public string Nazwisko { get; }
    public int Wiek { get; }

    // ...existing code...

    public static Result<Person> Create(string imie, string nazwisko, int wiek)
    {
        if (string.IsNullOrWhiteSpace(imie) || HasDigit(imie))
            return Result.Failure<Person>("Imie nie może być puste ani zawierać cyfr.");

        if (string.IsNullOrWhiteSpace(nazwisko) || HasDigit(nazwisko))
            return Result.Failure<Person>("Nazwisko nie może być puste ani zawierać cyfr.");

        if (wiek < 1 || wiek > 100)
            return Result.Failure<Person>("Wiek musi być pomiędzy 1 a 100.");

        return Result.Success(new Person(imie, nazwisko, wiek));
    }

    private static bool HasDigit(string value)
    {
        foreach (var c in value)
        {
            if (char.IsDigit(c))
                return true;
        }
        return false;
    }

    private Person(string imie, string nazwisko, int wiek)
    {


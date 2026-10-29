namespace AircraftIndustry.Components;

// компания-производитель авиационной техники
public class Manufacturer
{
    public string Name { get; }
    public string Country { get; }
    public int FoundationYear { get; }

    public Manufacturer(string name, string country, int foundationYear)
    {
        Name = name;
        Country = country;
        FoundationYear = foundationYear;
    }

    public override string ToString() => $"{Name} ({Country}, founded in {FoundationYear})";
}
namespace HeroPack.Classes;

public class Hero
{
    public string Name { get; init; }
    public List<Hand> Hands { get; } = new();
}

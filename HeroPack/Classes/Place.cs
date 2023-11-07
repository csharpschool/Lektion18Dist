using HeroPack.Interfaces;

namespace HeroPack.Classes;

public class Place
{
    public List<Character> Monsters { get; private set; } = new();
    public string Name { get; set; }
    public List<Attack> BattleLog { get; private set; } = new();
    public Backpack<IItem> Loot { get; private set; } = new(0);
    public Shop<IItem> Shop { get; private set; } = new();
    public Place? Next { get; private set; } = null;
    public Place? Previous { get; private set; } = null;

    public Place(string name, List<Character> monsters, Shop<IItem> shop, Character? boss = null)
        => (Name, Monsters, Shop) 
        = (name, AddMonsters(monsters, boss), shop);

    public void AddNextPlace(Place next) => Next = next;
    public void AddPreviousPlace(Place previous) => Previous = previous;

    List<Character> AddMonsters(List<Character> monsters, Character? boss)
    {
        var random = new Random();
        var numberOfMonsters = random.Next(monsters.Count);
        var indicies = new List<int>();

        while(indicies.Count < numberOfMonsters)
        {
            var index = random.Next(monsters.Count);
            if(!indicies.Contains(index))
                indicies.Add(index);
        }

        var adversaries = new List<Character>();
        indicies.OrderBy(i => i);
        foreach (var index in indicies)
        {
            adversaries.Add(monsters[index]);
            monsters.RemoveAt(index);
        }

        if(boss is not null)
            adversaries.Add(boss);

        return adversaries;
    }
}

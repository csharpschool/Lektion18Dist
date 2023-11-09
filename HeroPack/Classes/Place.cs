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
        var adversaries = new List<Character>();
        try
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

            /*indicies.OrderBy(i => i).ToList();
            foreach (var index in indicies)
            {
                adversaries.Add(monsters[index]);
                monsters.RemoveAt(index);
            }*/

            while(indicies.Count > 0)
            {
                var idx = indicies.Max();
                adversaries.Add(monsters[idx]);
                monsters.RemoveAt(idx);
                indicies.Remove(idx);
            }

            if(boss is not null)
                adversaries.Add(boss);
        }
        catch
        {
            throw;
        }

        return adversaries;
    }
}

/*
 * '   at System.Collections.Generic.List`1[[HeroPack.Classes.Character, HeroPack, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]].get_Item(Int32 index)\n   at HeroPack.Classes.Place.AddMonsters(List`1 monsters, Character boss) in C:\\Users\\ceshe\\source\\repos\\Lektion18Dist\\HeroPack\\Classes\\Place.cs:line 41'
 */
namespace HeroPack.Classes;

public class Spell
{
    public string Name { get; set; }
    public int MinDamage { get; set; }
    public int MaxDamage { get; set; }
    public double ManaCost { get; set; }

    public Spell(string name, int maxDamage, int minDamage, double manaCost)
    {
        Name = name;
        MinDamage = minDamage;
        MaxDamage = maxDamage;
        ManaCost = manaCost;
    }
}


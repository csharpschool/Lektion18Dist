using HeroPack.Interfaces;

namespace HeroPack.Classes.Weapons;

public class Weapon : Item, IDamage
{
    public double BaseDamage { get; }

    public Weapon(int id, Uri image, string name, double size, 
        int noOfHands, double durability, double baseDamage, double price, double dropProbability)
        : base(id, image, name, size, noOfHands, durability, 1, price, dropProbability)
    {
        BaseDamage = baseDamage;
    }

    public virtual double CalculateDamage(Character character)
    {
        var random = new Random();
        var damageRange = (random.NextDouble() / 4) + 0.85;
        return BaseDamage * damageRange;
    }

}

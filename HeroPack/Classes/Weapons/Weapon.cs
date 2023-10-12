using HeroPack.Interfaces;

namespace HeroPack.Classes.Weapons;

public class Weapon : Item, IDamage
{
    public double BaseDamage { get; }

    public Weapon(int id, Uri image, string name, double size, 
        int noOfHands, double durability, double baseDamage)
        : base(id, image, name, size, noOfHands, durability)
    {
        BaseDamage = baseDamage;
    }

    public virtual double CalculateDamage(ICharacter character)
    {
        return BaseDamage;
    }

}

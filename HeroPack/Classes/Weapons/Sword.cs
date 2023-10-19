using HeroPack.Interfaces;

namespace HeroPack.Classes.Weapons;

public class Sword : Weapon
{
    public Sword(int id, Uri image, string name, 
        int size, int noOfHands, double durability, double baseDamage) : 
        base(id, image, name, size, noOfHands, durability, baseDamage)
    {
    }

    public override double CalculateDamage(Character character)
    {
        return character.Strength * BaseDamage * Durability;
    }
}

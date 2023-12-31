﻿using HeroPack.Interfaces;

namespace HeroPack.Classes.Weapons;

public class Sword : Weapon
{
    public Sword(int id, Uri image, string name, 
        int size, int noOfHands, double durability, double baseDamage, double price, double dropProbability) : 
        base(id, image, name, size, noOfHands, durability, baseDamage, price, dropProbability)
    {
    }

    public override double CalculateDamage(Character character)
    {
        var baseDamage = base.CalculateDamage(character);
        return character.Strength * baseDamage * Durability;
    }
}

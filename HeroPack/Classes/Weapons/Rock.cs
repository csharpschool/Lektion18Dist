﻿namespace HeroPack.Classes.Weapons;

public class Rock : Weapon
{
    public Rock(int id, Uri image, string name,
    int size, int noOfHands, double durability, double baseDamage) :
    base(id, image, name, size, noOfHands, durability, baseDamage)
    {
    }

}
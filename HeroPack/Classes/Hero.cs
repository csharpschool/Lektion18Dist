using HeroPack.Classes.Weapons;
using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Net;
using System.Reflection.Metadata;

namespace HeroPack.Classes;

public class Hero : Character
{
    public Hero(string name, int numberOfHands, double strength, double stamina, double health)
    {
        Name = name;
        Hands = new(numberOfHands);
        CreateBackpack(36);
        for (int i = 0; i < numberOfHands; i++)
            Hands.Add(new Hand(4));
        Strength = strength;
        Stamina = stamina;
        Health = health;
    }
}

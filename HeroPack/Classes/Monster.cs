using HeroPack.Classes.Weapons;
using HeroPack.Interfaces;
using System.Xml.Linq;

namespace HeroPack.Classes;

public class Monster : Character
{
    public Monster(string name, int numberOfHands, int backpackSize, double strength, double stamina, double health)
    {
        Hands = new(numberOfHands);
        CreateBackpack(backpackSize);
        for (int i = 0; i < numberOfHands; i++)
            Hands.Add(new Hand(4));
        Strength = strength;
        Stamina = stamina;
        Health = health;
    }
}

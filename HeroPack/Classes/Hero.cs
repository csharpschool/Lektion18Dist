using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Net;

namespace HeroPack.Classes;

public class Hero : ICharacter
{
    public string Name { get; init; }
    public List<Hand> Hands { get; } = new();
    public double Strength { get; set; }
    public double Stamina { get; set; }


    //private bool AllHandsAreFree => Hands.Sum(h => h.Item?.Size) == 0;

    public Hero(int numberOfHands, double strength, double stamina)
    {
        Hands = new(numberOfHands);
        Strength = strength;
        Stamina = stamina;
    }

    /*private void AddToHand(Hand hand, IItem item) =>
        hand.Item = item;*/

    private void AddToHands(List<Hand> hands, IItem item)
    {
        foreach (var hand in hands)
            hand.Item = item;
    }

    private List<Hand>? GetFreeHands(int count)
    {
        var freeHands = 
            Hands.Where(h => h.Item is null).Take(count).ToList();

        if (freeHands.Count < count)
            return null;
        
        /*if (freeHands.Count < count)
            throw new HandException("Too few available hands.");*/

        return freeHands;
    }

    public bool PickUp(IItem item)
    {
        try
        {
            var freeHands = GetFreeHands(item.NoOfHands);
            if(freeHands is null) return false;

            AddToHands(freeHands, item);
            return true;
        }
        /*catch(HandException)
        {
            // Vad ska vi göra om det finns för få händer?
        }*/
        catch
        {
            return false;
        }
    }
}

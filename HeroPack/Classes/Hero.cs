using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Net;

namespace HeroPack.Classes;

public class Hero
{
    public string Name { get; init; }
    public List<Hand> Hands { get; } = new();

    //private bool AllHandsAreFree => Hands.Sum(h => h.Item?.Size) == 0;

    public Hero(int numberOfHands)
    {
        Hands = new(numberOfHands);
    }

    /*private void AddToHand(Hand hand, IItem item) =>
        hand.Item = item;*/

    private void AddToHands(List<Hand> hands, IItem item)
    {
        foreach (var hand in hands)
            hand.Item = item;
    }

    private List<Hand> GetFreeHands(int count)
    {
        var freeHands = 
            Hands.Where(h => h.Item is null).Take(count).ToList();

        if (freeHands.Count < count)
            throw new HandException("Too few available hands.");

        return freeHands;
    }

    public void PickUp(IItem item)
    {
        //var totalHandSize = Hands.Sum(h => h.MaxSize);
        //var totalItemSize = Hands.Sum(h => h.Item?.Size);
        //var freeHandSpace = Hands.Sum(h => h.MaxSize - h.Item?.Size);

        try
        {
            var freeHands = GetFreeHands(item.NoOfHands);
            AddToHands(freeHands, item);
        }
        catch(HandException)
        {
            // Vad ska vi göra om det finns för få händer?
        }
        catch
        {
            throw;
        }

        //var freeHand = Hands.Sum(h => h.MaxSize - h.Item?.Size);

        //if (item.Size > freeHandSpace)
            //throw new ItemException("Item too large to pick up.", item);
        
        // Gör något
        

    }
}

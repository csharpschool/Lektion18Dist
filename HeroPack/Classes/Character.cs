using HeroPack.Classes.Weapons;
using HeroPack.Interfaces;
using System.Reflection.Metadata;

namespace HeroPack.Classes;

public abstract class Character// : ICharacter
{
    public double Strength { get; init; }
    public double Stamina { get; init; }
    protected List<Hand> Hands { get; init; } = new();

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
            if (freeHands is null) return false;

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

    public Weapon? FindBestWeapon()
    {
        var weapons = Hands
            .Where(i => i.Item is Weapon)
            .Select(w => w.Item)
            .Cast<Weapon>();

        var bestWeapon = weapons
            .OrderByDescending(w => w.CalculateDamage(this))
            .FirstOrDefault();

        return bestWeapon;
    }
}

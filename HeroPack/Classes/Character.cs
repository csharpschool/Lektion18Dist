using HeroPack.Classes.Weapons;
using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Reflection.Metadata;

namespace HeroPack.Classes;

public abstract class Character : ICharacter
{
    public double Strength { get; init; }
    public double Stamina { get; init; }
    protected List<Hand> Hands { get; init; } = new();
    protected Backpack<IItem>? Backpack { get; private set; }

    protected void CreateBackpack(int size) => Backpack = new(size);
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

    public bool PickUp(Backpack<IItem> loot, IItem item)
    {
        try
        {
            ///TODO: Se till att man kan plocka upp så många rubiner som får plats i händerna och lämna resten i loot listan
            var freeHands = GetFreeHands(item.NoOfHands);
            if (freeHands is null) return false;

            AddToHands(freeHands, item);
            loot.Remove(item);
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

    public Backpack<IItem>? OpenBackpack() => Backpack;
    public List<Hand> GetItemsInHands() => Hands;

    public void AddToBackpack(Backpack<IItem> loot, IItem item)
    {
        try
        {
            Backpack?.Add(item);
            loot.Remove(item);
        }
        catch (ItemException)
        {
            throw;
        }
        catch
        {
            throw new ItemException("Could not add to backpack.");
        }
    }

    public Backpack<IItem>? Loot()
    {
        var backpack = Backpack;
        Backpack = null;
        return backpack;
    }
    
}

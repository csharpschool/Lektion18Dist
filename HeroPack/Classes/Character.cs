using HeroPack.Classes.Valuables;
using HeroPack.Classes.Weapons;
using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Reflection.Metadata;

namespace HeroPack.Classes;

public abstract class Character : ICharacter
{
    public double Strength { get; set; }
    public double Stamina { get; set; }
    public double Health { get; set; }
    public string Name { get; init; }
    protected List<Hand> Hands { get; init; } = new();
    protected Backpack<IItem>? Backpack { get; private set; }


    protected void CreateBackpack(int size) => Backpack = new(size);
    private void AddToHands(List<Hand> hands, IItem item)
    {
        foreach (var hand in hands)
            hand.Item = item;
    }
    private void AddToHands(List<Hand> hands, List<IItem> handItems)
    {
        if(hands.Count < handItems.Count)
            new HandException("Wrong number of hands.");

        for (int i = 0; i < handItems.Count; i++)
            hands[i].Item = handItems[i];
    }

    private List<Hand>? GetFreeHands(IItem item)
    {
        var freeHands =
            Hands.Where(h => h.Item is null).ToList();

        if(item is Valuable)
            return freeHands;

        freeHands = freeHands.Take(item.NoOfHands).ToList();

        if (freeHands.Count < item.NoOfHands)
            return null;

        /*if (freeHands.Count < count)
            throw new HandException("Too few available hands.");*/

        return freeHands;
    }

    public bool PickUp(Backpack<IItem> loot, IItem item)
    {
        try
        {
            var freeHands = GetFreeHands(item);
            if (freeHands is null) return false;

            if (item is Valuable)
            {
                List<IItem> handItems = new();
                IItem? lootItem = null;
                (handItems, lootItem) = DivideValuable(freeHands.Count, item);
                AddToHands(freeHands, handItems);
                if(lootItem is not null) 
                    loot.Add(lootItem);
            }
            else
            {
                AddToHands(freeHands, item);
            }

            loot.Remove(item);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private (List<IItem> HandItems, IItem? RemainingItem) 
        DivideValuable(int noFreeHands, IItem item)
    {
        var possibleToPickUp = (int)(noFreeHands / item.Size);
        var remaining = item.Quantity - possibleToPickUp;
        var handItems = new List<IItem>();
        var id = Backpack is null || Backpack.Count == 0 ? 1 : Backpack.Max(b => b.Id) + 1;
        var qty = possibleToPickUp / noFreeHands;
        for (int i = 0; i < noFreeHands; i++)
        {
            var newItem = new Valuable(
                id, new Uri("https://getbootstrap.com/"), item.Name,
                item.Size, qty, item.Durability);

            handItems.Add(newItem);
        }

        if (noFreeHands >= item.NoOfHands) return (handItems, null);

        id = Backpack is null || Backpack.Count == 0 ? 1 : Backpack.Max(b => b.Id) + 100;
        
        return (handItems, new Valuable(
            id, new Uri("https://getbootstrap.com/"), item.Name,
            item.Size, remaining, item.Durability));
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

    public Attack Attack(List<Character> adversaries)
    {
        try
        {
            var adversaryMinHealth = adversaries
                .Where(a => a.Health > 0)
                .Min(m => m.Health);
            var adversary = adversaries.First(m => m.Health == adversaryMinHealth);
            var bestWeapon = FindBestWeapon();

            var damage = bestWeapon is null 
                ? new Random().NextDouble() * 10 // Slåss utan vapen
                : bestWeapon.CalculateDamage(this);

            adversary.Health -= damage;
            if (adversary.Health < 0) adversary.Health = 0;

            return new Attack()
            {
                AttackerName = Name,
                AttackerHealth = Health,
                AdversaryHealth = adversary.Health,
                AdversaryName = adversary.Name,
                Damage = damage
            };
        }
        catch
        {
            throw new AttackException("No adversary to fight.");
        }
    }
}

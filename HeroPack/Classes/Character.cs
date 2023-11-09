using HeroPack.Classes.Consumables;
using HeroPack.Classes.Valuables;
using HeroPack.Classes.Weapons;
using HeroPack.Exceptions;
using HeroPack.Interfaces;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Linq;
using System.Reflection.Metadata;

namespace HeroPack.Classes;

public abstract class Character : ICharacter
{
    public double Strength { get; set; }
    public double Stamina { get; set; }
    public double Health { get; set; }
    public string Name { get; init; }
    public double Mana { get; set; }
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
                item.Size, qty, item.Durability, item.Price, item.DropProbability);

            handItems.Add(newItem);
        }

        if (noFreeHands >= item.NoOfHands) return (handItems, null);

        id = Backpack is null || Backpack.Count == 0 ? 1 : Backpack.Max(b => b.Id) + 100;
        
        return (handItems, new Valuable(
            id, new Uri("https://getbootstrap.com/"), item.Name,
            item.Size, remaining, item.Durability, item.Price, item.DropProbability));
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

    /*public void AddToBackpack(Backpack<IItem> loot, IItem item)
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
    }*/

    public void AddToBackpack(Backpack<IItem> loot, IItem item)
    {
        try
        {
            if(item.GetType() == typeof(Coin))
            {
                var gold = Backpack?.SingleOrDefault(
                    g => g.GetType() == typeof(Coin));

                if (gold is null)
                    Backpack?.Add(item);
                else
                    gold.Quantity += item.Quantity;
            }
            else
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
        var random = new Random();
                
        var droppableItems = Backpack?.Where(
            i => i.DropProbability > random.NextDouble());

        var items = new Backpack<IItem>(
            droppableItems is null ? 0: droppableItems.Count());

        if(droppableItems is not null)
            items.AddRange(droppableItems);

        Backpack = null;
        return items;
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

    public (bool Drank, string Message) Drink(Type item)
    {
        var potion = Backpack?.FirstOrDefault(p => p.GetType() == item);

        if (potion is null) return (false, "");

        Health += ((HealthPotion)potion).Capacity;
        Backpack?.Remove(potion);
        return (true, $"Drank a {item.Name} potion");
    }

    public string Purchace(Shop<IItem> shop, IItem item)
    {
        if (item.Size > Backpack?.FreeSpace)
            return "Does not fit in the backpack.";

        var gold = Backpack?.SingleOrDefault(
            g => g.GetType() == typeof(Coin));

        if (gold is null)
            return "No gold.";

        if (item.Price > gold.Quantity)
            return "Not enough gold.";

        gold.Quantity -= (int)Math.Ceiling(item.Price);

        Backpack?.Add(item);
        shop.Remove(item);

        return $"Purchased {item.Name}.";
    }

    public string Sell(Shop<IItem> shop, IItem itemFromBackpack)
    {
        var priceModifier = new Random().NextDouble();
        var coins = itemFromBackpack.Price - 
            itemFromBackpack.Price * priceModifier;

        if(coins < itemFromBackpack.Price * 0.5)
        {
            priceModifier = new Random().NextDouble();
            coins = itemFromBackpack.Price -
            itemFromBackpack.Price * priceModifier;
        }

        var gold = Backpack?.SingleOrDefault(
            g => g.GetType() == typeof(Coin));

        if (gold is null)
        {
            gold = new Coin(105, new Uri("https://getbootstrap.com/"), 
                "Gold Coins", (int)Math.Ceiling(coins), 100, 1, 1);
            Backpack?.Add(gold);
        }
        else
            gold.Quantity += (int)Math.Ceiling(coins);

        Backpack?.Remove(itemFromBackpack);
        shop.Add(itemFromBackpack);

        return $"Sold {itemFromBackpack.Name} for {Math.Round(coins, 2)} gold.";
    }

}


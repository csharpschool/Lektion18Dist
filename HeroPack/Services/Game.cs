﻿using HeroPack.Classes.Weapons;
using HeroPack.Classes;
using HeroPack.Interfaces;
using HeroPack.Classes.Valuables;
using System.Threading;
using HeroPack.Exceptions;
using HeroPack.Classes.Consumables;
using System.Numerics;
using HeroPack.Enums;
using System.Reflection.Emit;
using System.Linq;

namespace HeroPack.Services;

/// TODO: 1. Action points för mana, health, byta vapen
/// TODO: 3. Använda Stamina
/// TODO: 4. Kunna anv. Magi och Mana

public class Game
{
    public List<Place> Places { get; init; } = new();
    public Place? CurrentPlace { get; private set; } = null;
    public Character Hero { get; private set; } = new Hero("Balder", 2, 45, 35, 100, 75);
    public Character Boss { get; private set; } = new Monster("Skull Cracker", 2, 45, 200, 100, 300, 100);
    public List<Character> Monsters { get; private set; } = new()
    { 
        new Monster("Grog", 2, 10, 67, 35, 100, 12),
        new Monster("Floof", 2, 10, 67, 35, 100, 34),
        new Monster("Toof", 2, 10, 67, 35, 100),
        new Monster("Kaloof", 2, 10, 67, 35, 100)

    };
    public string Message { get; set; } = string.Empty;
    public Backpack<IItem> HerosBackpack { get; private set; } = new(0);
    //public List<Attack> BattleLog { get; private set; } = new();
    //public Backpack<IItem> Loot { get; private set; } = new(0);
    public Shop<IItem> Shop { get; private set; } = new();
    public (double AttackerHealth, double AdversaryHealth, string Error) AttackValues { get; set; }

    public Game()
    {
        try
        {
            Rock rock = new(1, new Uri("https://getbootstrap.com/"), "The Rock", 2, 1, 0.65, 0.75, 1);
            Sword sword = new(2, new Uri("https://getbootstrap.com/"), "Jack", 3, 1, 1, 1, 100, 0.25);
            Sword scimitar = new(3, new Uri("https://getbootstrap.com/"), "Big Bob", 3, 1, 1, 1, 100, 0.25);
            HealthPotion health = new(50, 1001, new Uri("https://getbootstrap.com/"), "Health Potion", 1, 1, 5, 35, 0.5);
            HealthPotion largeHealth = new(500, 1003, new Uri("https://getbootstrap.com/"), "Large Health Potion", 1, 1, 5, 35, 0.5);

            // Add to monster's Backpack
            //int id, Uri image, string name, int quantity, double durability
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Ruby(
                    101, new Uri("https://getbootstrap.com/"), "Large Ruby", 3, 100, 75, 0.7));
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Coin(
                    102, new Uri("https://getbootstrap.com/"), "Gold Coin", 100, 100, 1, 1));
            Monsters[1].AddToBackpack(new Backpack<IItem>(0), new Coin(
                    103, new Uri("https://getbootstrap.com/"), "Gold Coin", 10, 100, 1, 1));
            Monsters[1].PickUp(new Backpack<IItem>(0), scimitar);
            Boss.Spellbook.Add(new Spell("Crack and boom", 50, 15, 22.45));
            Boss.Spellbook.Add(new Spell("Zoom and boom", 75, 15, 80.55));

            // Add to Hero's Backpack
            Hero.AddToBackpack(new Backpack<IItem>(0), rock);
            Hero.AddToBackpack(new Backpack<IItem>(0), health);
            Hero.Spellbook.Add(new Spell("Fizzle and Pop", 75, 15, 20.45));
            //Hero.PickUp(new Backpack<IItem>(0), sword);

            // Add to Shop
            Shop.Add(new Ruby(101, new Uri("https://getbootstrap.com/"),
                "Large Ruby", 3, 100, 105, 0.3));
            Shop.Add(largeHealth);

            HerosBackpack = Hero.OpenBackpack() ?? new(0);
            //Loot = Monsters[0].Loot() ?? new(0);

            var place1 = new Place("City", Monsters, Shop);
            var place2 = new Place("Hamlet", Monsters, Shop);
            var place3 = new Place("The Dungeon", Monsters, Shop, Boss);
            place1.AddNextPlace(place2);
            place2.AddPreviousPlace(place1);
            place2.AddNextPlace(place3);
            place3.AddPreviousPlace(place2);

            Places.Add(place1);
            Places.Add(place2);
            Places.Add(place3);
            CurrentPlace = place1;
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    private bool DrinkHealthPotion(int level)
    {
        if (Hero.Health <= level)
        { 
            var (drank, message) = Hero.Drink(typeof(HealthPotion));
            if (!drank) Attack(Hero, CurrentPlace?.Monsters, null);
            else CurrentPlace?.BattleLog.Add(new Attack(message, Hero.Name, Hero.Health));
        }

        return Hero.Health <= level;
    }

    public async Task Action()
    {
        try
        {
            // Hjältens runda
            var drank = DrinkHealthPotion(25);
            if (!drank) Attack(Hero, CurrentPlace?.Monsters, null);

            await Task.Delay(500);
            if (CurrentPlace is not null && CurrentPlace?.Monsters is not null)
            {
                foreach (var monster in CurrentPlace.Monsters)
                {
                    if (monster.Health == 0)
                    {
                        CurrentPlace?.Loot.AddRange(monster.Loot() ?? new(0));
                        continue;
                    }

                    Attack(monster, new List<Character>() { Hero }, null);
                    if (Hero.Health <= 0)
                    {
                        Message = "You died!";
                        return;
                    }
                    await Task.Delay(500);
                }
            }
            ///TODO: Ta bort döda monster
        }
        catch (AttackException ex)
        {
            Message = ex.Message;
        }
    }

    public async Task Action(Spell spell)
    {
        try
        {
            // Hjältens runda
            var drank = DrinkHealthPotion(25);
            if (!drank) Attack(Hero, CurrentPlace?.Monsters, spell);
            await Task.Delay(500);

            if (CurrentPlace is not null && CurrentPlace?.Monsters is not null)
            {
                foreach (var monster in CurrentPlace.Monsters)
                {
                    if (monster.Health == 0)
                    {
                        CurrentPlace?.Loot.AddRange(monster.Loot() ?? new(0));
                        continue;
                    }

                    if (monster.Spellbook.Count > 0)
                    {
                        var spells = monster.Spellbook
                            .Where(s => s.ManaCost <= monster.Mana);
                        if(spells.Any()) 
                        {
                            var maxDamage = spells.Max(
                                s => s.MaxDamage);
                            var monsterSpell = monster.Spellbook.FirstOrDefault(
                                s => s.MaxDamage == maxDamage);
                            if(monsterSpell is not null && 
                                monsterSpell.ManaCost <= monster.Mana)
                                monster.MagicalAttack(
                                    new List<Character>() { Hero }, monsterSpell);
                            else
                                monster.Attack(new List<Character>() { Hero });
                        }
                        else
                            monster.Attack(new List<Character>() { Hero });
                    }
                    else
                        monster.Attack(new List<Character>() { Hero });

                    if (Hero.Health <= 0)
                    {
                        Message = "You died!";
                        return;
                    }
                    await Task.Delay(500);
                }
            }
        }
        catch (AttackException ex)
        {
            Message = ex.Message;
        }
    }

    public void Attack(Character attacker, List<Character>? 
    adversaries, Spell? spell)
    {
        try
        {
            if (adversaries is not null)
            {
                if (spell is null)
                    CurrentPlace?.BattleLog.Add(attacker.Attack(
                        new List<Character>(adversaries)));
                else
                    CurrentPlace?.BattleLog.Add(attacker.MagicalAttack(
                        new List<Character>(adversaries), spell));
            }
        }
        catch
        {
            throw;
        }
    }

    public void Move(Directions direction)
    {
        if(direction == Directions.Next && 
            CurrentPlace.Next is not null)
            CurrentPlace = CurrentPlace.Next;
        else if (direction == Directions.Previous &&
            CurrentPlace.Previous is not null)
            CurrentPlace = CurrentPlace.Previous;
    }
}

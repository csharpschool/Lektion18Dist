using HeroPack.Classes.Weapons;
using HeroPack.Classes;
using HeroPack.Interfaces;
using HeroPack.Classes.Valuables;
using System.Threading;
using HeroPack.Exceptions;
using HeroPack.Classes.Consumables;
using System.Numerics;
using HeroPack.Enums;

namespace HeroPack.Services;

/// TODO: 1. Action points för mana, health, byta vapen
/// TODO: 2. Möta en boss

public class Game
{
    public List<Place> Places { get; init; } = new();
    public Place? CurrentPlace { get; private set; } = null;
    public Character Hero { get; private set; } = new Hero("Balder", 2, 45, 35, 100);
    public List<Character> Monsters { get; private set; } = new()
    { 
        new Monster("Grog", 2, 10, 67, 35, 100),
        new Monster("Floof", 2, 10, 67, 35, 100),
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

            // Add to monster's Backpack
            //int id, Uri image, string name, int quantity, double durability
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Ruby(
                    101, new Uri("https://getbootstrap.com/"), "Large Ruby", 3, 100, 75, 0.7));
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Coin(
                    102, new Uri("https://getbootstrap.com/"), "Gold Coin", 100, 100, 1, 1));
            Monsters[1].AddToBackpack(new Backpack<IItem>(0), new Coin(
                    103, new Uri("https://getbootstrap.com/"), "Gold Coin", 10, 100, 1, 1));
            Monsters[1].PickUp(new Backpack<IItem>(0), scimitar);

            // Add to Hero's Backpack
            Hero.AddToBackpack(new Backpack<IItem>(0), rock);
            Hero.AddToBackpack(new Backpack<IItem>(0), health);
            //Hero.PickUp(new Backpack<IItem>(0), sword);

            // Add to Shop
            Shop.Add(new Ruby(101, new Uri("https://getbootstrap.com/"),
                "Large Ruby", 3, 100, 105, 0.3));

            HerosBackpack = Hero.OpenBackpack() ?? new(0);
            //Loot = Monsters[0].Loot() ?? new(0);

            var place1 = new Place("City", Monsters, Shop);
            var place2 = new Place("Hamlet", Monsters, Shop);
            place1.AddNextPlace(place2);
            place2.AddPreviousPlace(place1);

            Places.Add(place1);
            Places.Add(place2);
            CurrentPlace = place1;
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    public async Task Action()
    {
        try
        {
            // Hjältens runda
            if (Hero.Health > 75) Attack(Hero, Monsters);
            else
            {
                var (drank, message) = Hero.Drink(typeof(HealthPotion));
                if(!drank) Attack(Hero, Monsters);
                else CurrentPlace.BattleLog.Add(new Attack(message, Hero.Name, Hero.Health));
            }

            await Task.Delay(500);
            foreach (var monster in Monsters)
            {
                if (monster.Health == 0)
                {
                    CurrentPlace.Loot.AddRange(monster.Loot() ?? new(0));
                    continue;
                }

                Attack(monster, new List<Character>() { Hero });
                if(Hero.Health <= 0)
                {
                    Message = "You died!";
                    return;
                }
                await Task.Delay(500);
            }
            ///TODO: Ta bort döda monster
        }
        catch (AttackException ex)
        {
            Message = ex.Message;
        }
    }

    public void Attack(Character attacker, List<Character> adversaries)
    {
        try
        {
            CurrentPlace.BattleLog.Add(attacker.Attack(
                new List<Character>(adversaries)));
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

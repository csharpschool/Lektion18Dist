using HeroPack.Classes.Weapons;
using HeroPack.Classes;
using HeroPack.Interfaces;
using HeroPack.Classes.Valuables;
using System.Threading;
using HeroPack.Exceptions;

namespace HeroPack.Services;

/// TODO: 1. Monster slåss tillbaka
/// TODO: 2. Action points för mana, health, byta vapen

public class Game
{
    public Character Hero { get; private set; } = new Hero("Balder", 2, 45, 35, 100);
    public List<Character> Monsters { get; private set; } = new()
    { 
        new Monster("Grog", 2, 10, 67, 35, 100),
        new Monster("Floof", 2, 10, 67, 35, 100)
    };
    public List<Attack> BattleLog { get; private set; } = new();
    public string Message { get; set; } = string.Empty;
    public Backpack<IItem> HerosBackpack { get; private set; } = new(0);
    public Backpack<IItem> Loot { get; private set; } = new(0);
    public (double AttackerHealth, double AdversaryHealth, string Error) AttackValues { get; set; }

    public Game()
    {
        try
        {
            Rock rock = new(1, new Uri("https://getbootstrap.com/"), "The Rock", 2, 1, 0.65, 0.75);
            Sword sword = new(2, new Uri("https://getbootstrap.com/"), "Jack", 3, 1, 1, 1);

            // Add to monster's Backpack
            //int id, Uri image, string name, int quantity, double durability
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Ruby(
                    101, new Uri("https://getbootstrap.com/"), "Large Ruby", 3, 100));
            Monsters[0].AddToBackpack(new Backpack<IItem>(0), new Coin(
                    102, new Uri("https://getbootstrap.com/"), "Gold Coin", 100, 100));

            // Add to Hero's Backpack
            Hero.AddToBackpack(new Backpack<IItem>(0), rock);
            Hero.PickUp(new Backpack<IItem>(0), sword);

            HerosBackpack = Hero.OpenBackpack() ?? new(0);
            Loot = Monsters[0].Loot() ?? new(0);
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
        
        
    }

    public async Task Attack()
    {
        try
        {
            //Attack attack = Hero.Attack(Monsters);
            BattleLog.Add(Hero.Attack(Monsters));
            await Task.Delay(1000);
            foreach (var monster in Monsters)
            {
                if (monster.Health == 0) continue;
                BattleLog.Add(monster.Attack(
                    new List<Character>() { Hero }));
                await Task.Delay(1000);
            }
        }
        catch (AttackException ex)
        {
            Message = ex.Message;
        }
    }
}

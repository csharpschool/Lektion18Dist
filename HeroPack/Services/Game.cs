using HeroPack.Classes.Weapons;
using HeroPack.Classes;
using HeroPack.Interfaces;
using HeroPack.Classes.Valuables;
using System.Threading;

namespace HeroPack.Services;

public class Game
{
    public Character Hero { get; private set; } = new Hero("Balder", 2, 45, 35);
    public Character Monster { get; private set; } = new Monster("Grog", 2, 10, 35, 25);
    public string Message { get; private set; } = string.Empty;
    public Backpack<IItem> HerosBackpack { get; private set; } = new(0);
    public Backpack<IItem> Loot { get; private set; } = new(0);

    public Game()
    {
        try
        {
            Rock rock = new(1, new Uri("https://getbootstrap.com/"), "The Rock", 2, 1, 0.65, 0.75);
            Sword sword = new(2, new Uri("https://getbootstrap.com/"), "Jack", 3, 1, 1, 1);

            // Add to monster's Backpack
            //int id, Uri image, string name, int quantity, double durability
            Monster.AddToBackpack(new Backpack<IItem>(0), new Ruby(
                101, new Uri("https://getbootstrap.com/"), "Large Ruby", 3, 100));
            Monster.AddToBackpack(new Backpack<IItem>(0), new Coin(
                102, new Uri("https://getbootstrap.com/"), "Gold Coin", 7, 100));

            // Add to Hero's Backpack
            Hero.AddToBackpack(new Backpack<IItem>(0), rock);
            Hero.AddToBackpack(new Backpack<IItem>(0), sword);

            HerosBackpack = Hero.OpenBackpack() ?? new(0);
            Loot = Monster.Loot() ?? new(0);
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
        
        
    }

}

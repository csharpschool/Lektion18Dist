using HeroPack.Interfaces;

namespace HeroPack.Classes;

public class Item : IItem
{
    public int Id { get; init; }
    public Uri Image { get; init; }
    public string Name { get; init; }
    public double Size { get; init; }
    public int NoOfHands { get; set; }
    public double Durability { get; set; }
    public int Quantity { get; set; }
    double price = 0;
    public double Price { 
        get { return price; } 
        set { price = value * Durability / 100; }
    }


    public Item(int id, Uri image, string name,
        double size, int noOfHands, double durability, int quantity, double price) 
        => (Id, Image, Name, Size, NoOfHands, Durability, Quantity, Price)
        = (id, image, name, size, noOfHands, durability, quantity, price);
}

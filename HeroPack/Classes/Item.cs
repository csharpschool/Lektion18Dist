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

    public Item(int id, Uri image, string name,
        double size, int noOfHands, double durability) 
        => (Id, Image, Name, Size, NoOfHands, Durability)
        = (id, image, name, size, noOfHands, durability);
}

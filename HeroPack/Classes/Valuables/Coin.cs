namespace HeroPack.Classes.Valuables;

public class Coin : Valuable
{
    public Coin(int id, Uri image, string name, int quantity, double durability, double price, double dropProbability)
        : base(id, image, name, 0.1, quantity, durability, price, dropProbability)
    {
    }
}

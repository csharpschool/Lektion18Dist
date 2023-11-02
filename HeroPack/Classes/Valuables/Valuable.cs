using HeroPack.Interfaces;

namespace HeroPack.Classes.Valuables
{
    public class Valuable : Item, IValuable
    {
        public Valuable(int id, Uri image, string name, double size, int quantity, double durability, double price, double dropProbability)
            : base(id, image, name, size, (int)Math.Ceiling(quantity * size), durability, quantity, price, dropProbability)
        {
        }

    }
}

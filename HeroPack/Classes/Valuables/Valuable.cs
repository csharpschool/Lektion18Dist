using HeroPack.Interfaces;

namespace HeroPack.Classes.Valuables
{
    public class Valuable : Item, IValuable
    {
        public int Quantity { get; set; }

        public Valuable(int id, Uri image, string name, double size, int quantity, double durability)
            : base(id, image, name, size, (int)(quantity * size), durability)
        {
            Quantity = quantity;
        }

    }
}

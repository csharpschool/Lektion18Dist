namespace HeroPack.Classes.Valuables
{
    public class Ruby : Valuable
    {
        public Ruby(int id, Uri image, string name, int quantity, double durability)
            : base(id, image, name, 0.25, quantity, durability)
        {
        }
    }
}

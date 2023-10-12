namespace HeroPack.Classes.Valuables
{
    public class Valuable : Item
    {
        public Valuable(int id, Uri image, string name, double size, int noOfHands, double durability)
            : base(id, image, name, size, noOfHands, durability)
        {
        }
    }
}

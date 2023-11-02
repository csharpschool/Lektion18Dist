using HeroPack.Interfaces;

namespace HeroPack.Classes.Consumables
{
    public class HealthPotion : Item
    {
        public int Capacity { get; init; }

        public HealthPotion(int capacity, int id, Uri image, 
            string name, double size, int noOfHands, double durability, double price)
        : base(id, image, name, size, noOfHands, durability, 1, price) 
            => Capacity = capacity;
            
    }
}

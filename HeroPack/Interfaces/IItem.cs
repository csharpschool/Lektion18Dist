namespace HeroPack.Interfaces
{
    public interface IItem
    {
        int Id { get; init; }
        Uri Image { get; init; }
        string Name { get; init; }
        double Size { get; init; }
        int NoOfHands { get; set; }
        double Durability { get; set; }
        int Quantity { get; set; }
    }
}

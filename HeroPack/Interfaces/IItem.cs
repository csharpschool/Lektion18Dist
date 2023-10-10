namespace HeroPack.Interfaces
{
    public interface IItem
    {
        string Name { get; init; }
        public int Size { get; init; }
        public int NoOfHands { get; set; }
    }
}

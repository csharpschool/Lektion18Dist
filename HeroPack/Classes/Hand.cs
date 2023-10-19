using HeroPack.Interfaces;

namespace HeroPack.Classes;

public class Hand
{
    public int MaxSize { get; set; }
    public IItem? Item { get; set; }

    public Hand(int maxSize) => MaxSize = maxSize;
}

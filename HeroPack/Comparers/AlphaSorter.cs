using HeroPack.Interfaces;

namespace HeroPack.Comparers;

public class AlphaSorter : IComparer<IItem>
{
    public int Compare(IItem? item1, IItem? item2)
    {
        if (item1 == null && item2 == null) return 0;
        if (item1 == null) return 1;
        if (item2 == null) return -1;

        return item1.Name.CompareTo(item2.Name);
    }
}

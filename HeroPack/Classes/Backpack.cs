using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Data;

namespace HeroPack.Classes;

public class Backpack
{
    public List<IItem> Items { get; } = new();
    public int MaxSize { get; private set; }

    public Backpack(int maxSize) => MaxSize = maxSize;    

    public int FreeSpace { 
        get
        {
            var occupied = Items.Sum(x => x.Size);
            return (int)(MaxSize - occupied);
        }
    }

    public void Sort(IComparer<IItem> comparer)
    {
        Items.Sort(comparer);
    }

    public void Remove(IItem item) => Items.Remove(item);

    public void Add(IItem item)
    {
        if (item.Size <= FreeSpace)
            Items.Add(item);
        else 
            throw new ItemException("För lite plats i rygsäcken", item);
    }
}

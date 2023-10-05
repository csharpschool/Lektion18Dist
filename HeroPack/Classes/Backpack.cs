using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Data;

namespace HeroPack.Classes;

public class Backpack
{
    public List<IItem> Items { get; } = new();
    public int MaxSize { get; init; }

    public int FreeSpace { 
        get
        {
            var occupied = Items.Sum(x => x.Size);
            return MaxSize - occupied;
        }
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

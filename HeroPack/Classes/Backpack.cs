using HeroPack.Exceptions;
using HeroPack.Interfaces;
using System.Data;

namespace HeroPack.Classes;

public class Backpack<T> : List<T> where T : class, IItem
{
    public int MaxSize { get; private set; }

    public Backpack(int maxSize) => MaxSize = maxSize;    

    public int FreeSpace { 
        get
        {
            var occupied = this.Sum(x => x.Size);
            return (int)(MaxSize - occupied);
        }
    }

    public new void Sort(IComparer<T> comparer)
    {
        base.Sort(comparer);
    }

    public new void Remove(T item) => base.Remove(item);

    public new void Add(T item)
    {
        if (item.Size <= FreeSpace)
            base.Add(item);
        else 
            throw new ItemException("För lite plats i rygsäcken", item);
    }
}

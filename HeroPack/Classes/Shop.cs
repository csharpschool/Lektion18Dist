using HeroPack.Interfaces;

namespace HeroPack.Classes;

public class Shop<T> : List<T> where T : class, IItem
{
    public List<T> Get(Func<T, bool>? expression)
    {
        if (expression is null) return this;

        return this.Where(expression).ToList();
    }
}

using HeroPack.Classes;

namespace HeroPack.Interfaces;

public interface IDamage
{
    double BaseDamage { get; }

    double CalculateDamage(Character character);
}

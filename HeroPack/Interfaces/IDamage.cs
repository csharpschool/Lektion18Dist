namespace HeroPack.Interfaces;

public interface IDamage
{
    double BaseDamage { get; }

    double CalculateDamage(ICharacter character);
}

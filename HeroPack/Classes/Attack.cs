using HeroPack.Enums;

namespace HeroPack.Classes;

public class Attack
{
    public double AttackerHealth { get; set; }
    public double AdversaryHealth { get; set; }
    public double Damage { get; set; }
    public string? AttackerName { get; set; }
    public string? AdversaryName { get; set; }
    public string? Message { get; set; }
    public AttackType Type { get; set; }

    public Attack() { }
    public Attack(string message, string attackerName, double attackerHealth, AttackType type = AttackType.Weapon) 
        => (Message, AttackerName, AttackerHealth, Type) = (message, attackerName, attackerHealth, type);
}

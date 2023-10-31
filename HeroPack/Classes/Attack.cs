namespace HeroPack.Classes;

public class Attack
{
    public double AttackerHealth { get; set; }
    public double AdversaryHealth { get; set; }
    public double Damage { get; set; }
    public string? AttackerName { get; set; }
    public string? AdversaryName { get; set; }
    public string? Message { get; set; }

    public Attack() { }
    public Attack(string message, string attackerName, double attackerHealth) 
        => (Message, AttackerName, AttackerHealth) = (message, attackerName, attackerHealth);
}

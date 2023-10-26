using HeroPack.Classes;
using HeroPack.Classes.Weapons;

namespace HeroPack.Interfaces;

public interface ICharacter
{
    double Strength { get; }
    double Stamina { get; }
    double Health { get; }
    bool PickUp(Backpack<IItem> loot, IItem item);
    Weapon? FindBestWeapon();
    Backpack<IItem>? Loot();
    void AddToBackpack(Backpack<IItem> loot, IItem item);
    (double AttackerHealth, double AdversaryHealth, string Error) Attack(List<Character> adversaries);
}

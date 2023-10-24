using HeroPack.Classes;
using HeroPack.Classes.Weapons;

namespace HeroPack.Interfaces;

public interface ICharacter
{
    double Strength { get; init; }
    double Stamina { get; init; }
    bool PickUp(Backpack<IItem> loot, IItem item);
    Weapon? FindBestWeapon();
    Backpack<IItem>? Loot();
    void AddToBackpack(Backpack<IItem> loot, IItem item);
}

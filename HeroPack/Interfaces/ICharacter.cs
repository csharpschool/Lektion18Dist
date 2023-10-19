using HeroPack.Classes.Weapons;

namespace HeroPack.Interfaces;

public interface ICharacter
{
    double Strength { get; set; }
    double Stamina { get; set; }
    bool PickUp(IItem item);
    Weapon? FindBestWeapon();
}

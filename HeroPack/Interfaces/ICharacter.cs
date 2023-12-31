﻿using HeroPack.Classes;
using HeroPack.Classes.Weapons;

namespace HeroPack.Interfaces;

public interface ICharacter
{
    double Strength { get; }
    double Stamina { get; }
    double Health { get; }
    double Mana { get; }
    List<Spell> Spellbook { get; set; }
    bool PickUp(Backpack<IItem> loot, IItem item);
    Weapon? FindBestWeapon();
    Backpack<IItem>? Loot();
    void AddToBackpack(Backpack<IItem> loot, IItem item);
    Attack Attack(List<Character> adversaries);
}

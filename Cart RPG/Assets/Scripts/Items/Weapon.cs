using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    private int damage;
    private DamageType damageType;
    private WeaponType weaponType;

    public int Damage {
        get { return damage; }
    }

    public DamageType DamageType {
        get { return damageType; }
    }
    public WeaponType WeaponType {
        get { return weaponType; }
    }

    public Weapon(int id, string title, string description, int value, ItemType itemType, string spritePath, bool stackable, WeaponType weaponType, int damage, DamageType damageType)
        : base(id, title, description, value, itemType, spritePath, stackable) {

        this.weaponType = weaponType;
        this.damage = damage;
        this.damageType = damageType;
    }

    public Weapon() {

    }
}

public enum WeaponType { Melee, Ranged, Magical }
public enum DamageType { Physical, Magical }

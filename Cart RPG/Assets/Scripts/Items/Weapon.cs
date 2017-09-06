using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {
    private int damage;
    private DamageType damageType;

    public int Damage {
        get { return damage; }
    }

    public DamageType DamageType {
        get { return damageType; }
    }

    public Weapon(string name, string description, int value, ItemType itemType, int damage, DamageType damageType)
        : base(name, description, value, itemType) {

        this.damage = damage;
        this.damageType = damageType;
    }
}

public enum DamageType { Physical, Magical }

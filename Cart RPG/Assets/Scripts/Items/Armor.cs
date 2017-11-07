using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item {
    private ArmorSlot armorSlot;
    private int bonusArmor;
    private int bonusVitality;
    private int bonusStrength;
    private int bonusIntelligence;
    private int bonusMovementSpeed;

    public Armor(int id, string title, string description, int value, ItemType itemType, string spritePath, bool stackable, ArmorSlot armorSlot, int bonusArmor, int bonusVitality, int bonusStrength, int bonusIntelligence, int bonusMovementSpeed)
        : base(id, title, description, value, itemType, spritePath, stackable) {

        this.armorSlot = armorSlot;
        this.bonusArmor = bonusArmor;
        this.bonusVitality = bonusVitality;
        this.bonusStrength = bonusStrength;
        this.bonusIntelligence = bonusIntelligence;
        this.bonusMovementSpeed = bonusMovementSpeed;
    }
    public Armor() {

    }

    public int BonusArmor {
        get { return bonusArmor; }
    }
    public int BonusVitality {
        get { return bonusVitality; }
    }
    public int BonusStrength {
        get { return bonusStrength; }
    }
    public int BonusIntelligence {
        get { return bonusIntelligence; }
    }
    public int BonusMovementSpeed {
        get { return bonusMovementSpeed; }
    }
}
public enum ArmorSlot { Head, Chest, Hands, Legs, Feet }
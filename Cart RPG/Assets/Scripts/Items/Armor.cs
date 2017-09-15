using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item {
    private int bonusArmor;
    private int bonusVitality;
    private int bonusStrength;
    private int bonusIntelligence;
    private int bonusMovementSpeed;

    public Armor(string title, string description, int value, ItemType itemType, int bonusArmor, int bonusVitality, int bonusStrength, int bonusIntelligence, int bonusMovementSpeed)
        : base(title, description, value, itemType) {

        this.bonusArmor = bonusArmor;
        this.bonusVitality = bonusVitality;
        this.bonusStrength = bonusStrength;
        this.bonusIntelligence = bonusIntelligence;
        this.bonusMovementSpeed = bonusMovementSpeed;
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
    public int BbonusIntelligence {
        get { return bonusIntelligence; }
    }
    public int BonusMovementSpeed {
        get { return bonusMovementSpeed; }
    }

}

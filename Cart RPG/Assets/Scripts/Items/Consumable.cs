using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {
    private int healthRecovery;
    private int manaRecovery;


    public Consumable(string name, string description, int value, ItemType itemType, int healthRecovery, int manaRecovery)
        : base(name, description, value, itemType) {

        this.healthRecovery = healthRecovery;
        this.manaRecovery = manaRecovery;
    }

    public int HealthRecovery {
        get { return healthRecovery; }
    }

    public int ManaRecovery {
        get { return manaRecovery; }
    }
}

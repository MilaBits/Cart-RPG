using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {
    private int healthRecovery;
    private int manaRecovery;


    public Consumable(int id, string title, string description, int value, ItemType itemType, string spritePath, bool stackable, int healthRecovery, int manaRecovery)
        : base(id, title, description, value, itemType, spritePath, stackable) {

        this.healthRecovery = healthRecovery;
        this.manaRecovery = manaRecovery;
    }
    public Consumable() {

    }

    public int HealthRecovery {
        get { return healthRecovery; }
    }

    public int ManaRecovery {
        get { return manaRecovery; }
    }
}

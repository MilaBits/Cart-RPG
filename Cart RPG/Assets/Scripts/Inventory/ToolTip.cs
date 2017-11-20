using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour {
    private Item item;
    private string data;

    private GameObject tooltip;

    public void Start() {

        tooltip = GameObject.Find("Tooltip");
        tooltip.SetActive(false);
    }

    public void Update() {
        if (tooltip.activeSelf) {
            tooltip.transform.position = Input.mousePosition;
        }
    }

    public void Activate(Item item) {

        this.item = item;
        tooltip.SetActive(true);
        ConstructDataString();
    }

    public void Deactivate() {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() {
        string color = "#f8e6b0";

        string title = string.Format("<color=#f8e6b0><size=20>{0}</size></color>", item.Title);
        string itemType = string.Format("<color=#b3b3b3>{0}</color>\n", item.ItemType);
        string value = string.Format("\n<color=#ffc100>{0} Coin </color>", item.Value);
        string description = string.Format("\n\n<color=#b3b3b3>{0}</color>\n", item.Description);
        string armorStats = string.Empty;
        string weaponStats = string.Empty;
        string ConsumableStats = string.Empty;

        switch (item.ItemType) {
            case ItemType.Armor:
                Armor armor = (Armor)item;
                if (armor.BonusArmor > 0)
                    armorStats += string.Format("\n<color=#f8e6b0>Armor: {0}</color>", armor.BonusArmor);
                if (armor.BonusStrength > 0)
                    armorStats += string.Format("\n<color=#f8e6b0>Strength: {0}</color>", armor.BonusStrength);
                if (armor.BonusVitality > 0)
                    armorStats += string.Format("\n<color=#f8e6b0>Vitality: {0}</color>", armor.BonusVitality);
                if (armor.BonusIntelligence > 0)
                    armorStats += string.Format("\n<color=#f8e6b0>Intelligence: {0}</color>", armor.BonusIntelligence);
                if (armor.BonusMovementSpeed > 0)
                    armorStats += string.Format("\n<color=#f8e6b0>Movement Speed: {0}</color>", armor.BonusMovementSpeed);
                break;
            case ItemType.Weapon:
                Weapon weapon = (Weapon)item;
                switch (weapon.WeaponType) {
                    case WeaponType.Melee:
                        color = "#c66108";
                        itemType = string.Format("\n<color={0}>{1} Weapon</color>", color, weapon.DamageType);
                        break;
                    case WeaponType.Ranged:
                        color = "#52ee39";
                        itemType = string.Format("\n<color={0}>{1} Weapon</color>", color, weapon.DamageType);
                        break;
                    case WeaponType.Magical:
                        color = "#d100ea";
                        itemType = string.Format("\n<color={0}>{1} Weapon</color>", color, weapon.DamageType);
                        break;
                }
                weaponStats += string.Format("\n<color=#f8e6b0>Damage:</color> <color={0}>{1} {2}</color>", color, weapon.Damage, weapon.DamageType);
                break;
            case ItemType.Consumable:
                Consumable consumable = (Consumable)item;
                if (consumable.HealthRecovery > 0) {
                    color = "#ff0000";
                    ConsumableStats += string.Format("\n<color=#f8e6b0>Health Recovery:</color> <color={0}>{1}</color>", color, consumable.HealthRecovery);
                }
                if (consumable.ManaRecovery > 0) {
                    color = "#0000ff";
                    ConsumableStats += string.Format("\n<color=#f8e6b0>Mana Recovery:</color> <color={0}>{1}</color>", color, consumable.HealthRecovery);
                }
                break;
            case ItemType.Junk:
                break;
        }

        data = title + value + itemType + armorStats + weaponStats + ConsumableStats + description;

        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
        Canvas.ForceUpdateCanvases();
    }
}

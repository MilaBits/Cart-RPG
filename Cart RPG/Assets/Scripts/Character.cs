using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    private bool player;

    public string name;
    public Race race;

    public int health;
    public int maxHealth;
    public int mana;
    public int maxMana;
    public int armor;

    public int vitality;
    public int strength;
    public int intelligence;

    public float movementSpeed;

    public int inventorySize;
    public List<Item> inventoryItems;
}

public enum Race { Human, Elf, Orc, Dwarf, Goblin }

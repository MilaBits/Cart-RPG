using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleData
{
    public int StorageId { get; set; }
    public ModuleType Type { get; set; }
    public int Position { get; set; }
    public int Rotation { get; set; }

    public ModuleData(int storageId, ModuleType type, int position, int rotation)
    {
        StorageId = storageId;
        Type = type;
        Position = position;
        Rotation = rotation;
    }
}

public enum ModuleType { Crate, CrateWide, Barrel, Cage, CageBig }

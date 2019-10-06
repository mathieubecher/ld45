using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlanType
{
    FOOD,TOOL, FURNITURE
}
[System.Serializable]
public struct Plan
{
    public PlanType type;
    public GameObject craftItem;
    public List<ItemInventory> plan;
}

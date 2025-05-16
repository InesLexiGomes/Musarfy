using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<uint> inventory = new List<uint>();

    public void AddItem(uint itemID)
    {
        inventory.Add(itemID);
    }

    public bool CheckItemInInventory(uint itemID)
    {
        return inventory.Contains(itemID);
    }
}

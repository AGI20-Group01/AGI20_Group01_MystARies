using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<BlockSciptableObject> blockList = new List<BlockSciptableObject>();
    public InventoryDatabase inventoryDatabase;
    public UIInventory inventoryui;

    // Start is called before the first frame update
    void Start()
    {
        GiveBlock(0);
        GiveBlock(0);
    }

    public void GiveBlock(int id)
    {
        BlockSciptableObject blockToAdd = inventoryDatabase.GetBlock(id);
        blockList.Add(blockToAdd);
        inventoryui.AddNewItem(blockToAdd);
        Debug.Log("Added block");
    }

    public void GiveBlock(string blockName)
    {
        BlockSciptableObject blockToAdd = inventoryDatabase.GetBlock(blockName);
        blockList.Add(blockToAdd);
        inventoryui.AddNewItem(blockToAdd);
    }

    public BlockSciptableObject CheckForBlock(int id)
    {
        return blockList.Find(block => block.id == id);
    }

    public void RemoveItem(int id)
    {
        BlockSciptableObject blockToRemove = CheckForBlock(id);
        if(blockToRemove != null)
        {
            blockList.Remove(blockToRemove);
            inventoryui.RemoveItem(blockToRemove);
        }
    }
}

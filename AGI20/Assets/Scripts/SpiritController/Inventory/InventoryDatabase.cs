using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDatabase : MonoBehaviour
{
    public List<BlockSciptableObject> blocks = new List<BlockSciptableObject>();

    private void Awake()
    {
        BuildDatabase();
    }

    public BlockSciptableObject GetBlock(int id)
    {
        return blocks.Find(block => block.id == id);
    }

    public BlockSciptableObject GetBlock(string blockName)
    {
        return blocks.Find(block => block.title == blockName);
    }

    void BuildDatabase()
    {
        blocks = new List<BlockSciptableObject>() {
                 new BlockSciptableObject(0, "Gold Ore", "Can freely be picked up and placed around the enviroment")
                 };
    }
}

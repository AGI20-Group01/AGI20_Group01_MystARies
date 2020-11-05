using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;

    void Awake()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, BlockSciptableObject block)
    {
        uiItems[slot].UpdateItem(block);
    }

    public void AddNewItem(BlockSciptableObject block)
    {
        UpdateSlot(uiItems.FindIndex(i => i.block == null), block);
    }

    public void RemoveItem(BlockSciptableObject block)
    {
        UpdateSlot(uiItems.FindIndex(i => i.block == block), null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerDownHandler
{
    public BlockSciptableObject block;
    private Image spriteImage;
    private UIItem selectedItem;
    void Awake()
    {
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
    }

    public void UpdateItem(BlockSciptableObject block)
    {
        this.block = block;
        if (this.block != null)
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = block.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.block != null)
        {
            if (selectedItem.block != null)
            {
                BlockSciptableObject clone = new BlockSciptableObject(selectedItem.block);
                selectedItem.UpdateItem(this.block);
                UpdateItem(clone);
            }
            else
            {
                selectedItem.UpdateItem(this.block);
                UpdateItem(null);
            }
        }
        else if (selectedItem.block != null)
        {
            UpdateItem(selectedItem.block);
            selectedItem.UpdateItem(null);
        }
    }
}

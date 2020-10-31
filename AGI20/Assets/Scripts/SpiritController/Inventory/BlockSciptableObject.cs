using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSciptableObject
{
    public int id;
    public string title;
    public string description;
    public Sprite icon;

    // One constructor for creating the sprite, another if we need to copy it
    public BlockSciptableObject(int id, string title, string description)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Assets/Resources/Sprites/TestInventory/" + title);
    }

    public BlockSciptableObject(BlockSciptableObject block)
    {
        this.id = block.id;
        this.title = block.title;
        this.description = block.description;
        this.icon = Resources.Load<Sprite>("Assets/Resources/Sprites/TestInventory/" + title);
    }
}

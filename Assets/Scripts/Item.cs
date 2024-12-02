using UnityEngine;

public class Item
{
    public string itemName;
    // public Sprite itemIcon;
    public int itemCount;
    public string itemDescription;

    public Item(string _itemName, string _itemDes, int _itemCount)
    {
        itemName = _itemName;
        // itemIcon = _itemIcon;
        itemDescription = _itemDes;
        itemCount = _itemCount;
    }
}

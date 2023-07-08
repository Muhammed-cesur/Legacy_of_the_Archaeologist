 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemcontroller : MonoBeahvior
{
    public Item item;

    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void addItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.potion:
                Player.Instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.book:
                Player.Instance.IncreaseExp(item.value);
                break;

        }
        RemoveItem();
    }
}
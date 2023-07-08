using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        Items.Remove(item);
    }

    public void ListItems()

    //Clean content before open.
    foreach (Transform item in ItemContent)
    {
        Destroy(item.gameObject);
    }

    foreach (var item in Items)
    {
        GameObject obj = Instantiate(InventoryItem, ItemContent);
        var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
        var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
    var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

        itemName.text = item.itemName;
        itemIcon.sprite = item.icon;

    if(EnableRemove.isOn)
        removeButton.gameObject.SetActive(true);
    }

public void EnableItems Remove()
{
    if (EnableRemove.ison)
    {
        foreach (Transform item in ItemContent)
        {
            item.Find("RemoveButton").gameObject.SetActive(true);
        }
    }

    else

    {
        foreach (Transform item in ItemContent)


        {
            item.Find("RemoveButton").gameObject.SetActive(false);
        }

    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item" ,menuName ="Item/Create New Item")]
public class Item : ScriptTableObject
{
    public int id;
    public string ÝtemName;
    public int value;
    public Sprite icon;
    public ItemType itemType;

    public enum ItemType;
		{
			Potion,
			book
}
}
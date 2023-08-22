using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This is the scriptable object that holds the data template for the items.
    [CreateAssetMenu(fileName = "New Item", menuName = "XEntity/Item")]
    public class Item : ScriptableObject
    {
        //**IMPORTANT** 
        //Do not change or remove the following properties in the "Essential region"
        //unless you are going to customize other dependent code to work with it.
        //You may add as many properties as you want after the essential region.

        #region Essential
        public ItemType type;
        public string itemName;
        public int itemPerSlot;
        public Sprite icon;
        public GameObject prefab;

        [TextArea]
        public string itemInformation;

        #endregion
    }
}

using UnityEngine;
using UnityEditor;

namespace XEntity.InventoryItemSystem.CustomEditors
{
    //This is an editor script that adds the selected item scriptable objects automatically to the ItemManager.itemList.
    //NOTE: This will avoid adding duplicate items to the list and anything selected that are not of the type Item will be ignored.
    //NOTE: An ItemManager must exist in the scene.
    public class ItemAdderEditor : Editor
    {
        [MenuItem("Assets/Add To Item List")]
        public static void AddItemToList()
        {
            ItemManager manager = FindObjectOfType<ItemManager>();

            if (manager == null)
            {
                Debug.Log("<color=#ff8080>Item Manager does not exist in the current scene</color>");
                return;
            }
            else
            {
                //Go through all the currently selected objects and add the eligible items to the list.
                foreach (Object obj in Selection.objects)
                {
                    if (obj.GetType() == typeof(Item))
                    {
                        Item item = (Item)obj;
                        if (manager.itemList.Contains(item)) Debug.Log($"<color=#80aaff>{item.name} already exists in the list.</color>");
                        else
                        {
                            manager.itemList.Add(item);
                            Debug.Log($"<color=#ccff99>{item.name} succesfully added to the list!</color>");
                        }
                    }
                    else Debug.Log($"<color=#ffd480>{obj.name} is not an item</color>");
                }

                EditorUtility.SetDirty(manager);
            }
        }
    } 
}

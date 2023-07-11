using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This script is attached to any container slots that can hold items.
    public class ItemSlot : MonoBehaviour
    {
        //The item currently held in this slot; null if there is none.
        public Item slotItem;

        //The quantity of items in this slot.
        public int itemCount;

        //Returns true if itemCount is zero.
        public bool IsEmpty { get { return itemCount <= 0; } }

        //The image for displaying the item icon.
        private UnityEngine.UI.Image iconImage;

        //The text for displaying the itemCount.
        private UnityEngine.UI.Text countText;

        //This is renamed from private void Awake()
        public void Initialize() 
        {
            //The UI variables are assigned here.
            iconImage = transform.Find("Icon Image").GetComponent<UnityEngine.UI.Image>();
            countText = transform.Find("Count Text").GetComponent<UnityEngine.UI.Text>();

            iconImage.gameObject.SetActive(false);
            countText.text = string.Empty;
        }

        //Returns true if its able to add the item to the slot.
        //NOTE: Items are stacked if they are of the same type.
        public bool Add(Item item) 
        {
            if (IsAddable(item))
            {
                slotItem = item;
                itemCount++;
                OnSlotModified();
                return true;
            }
            else return false; 
            
        }

        //Removes the passed in amount of items from the slot and drops them at the dropPosition.
        public void RemoveAndDrop(int amount, Vector3 dropPosition) 
        {
            for (int i = 0; i < amount; i++) 
            {
                if (itemCount > 0)
                {
                    Utils.InstantiateItemCollector(slotItem, dropPosition);
                    itemCount--;
                }
                else break;
            }

            OnSlotModified();
        }

        //Removes the passed in amount of items from the slot.
        public void Remove(int amount)
        {
            itemCount -= amount > itemCount ? itemCount : amount;
            OnSlotModified();
        }

        //Empties the slot completely.
        public void Clear() 
        {
            itemCount = 0;
            OnSlotModified();
        }

        //Empties the slot completely and drops all the items at the dropPosition.
        public void ClearAndDrop(Vector3 dropPosition) 
        {
            RemoveAndDrop(itemCount, dropPosition);
        }

        //Returns true if all the conditions are met for the item to be added/stacked.
        private bool IsAddable(Item item)
        {
            if (item != null)
            {
                if (IsEmpty) return true;
                else
                {
                    if (item == slotItem && itemCount < item.itemPerSlot) return true;
                    else return false;
                }
            }
            return false;
        }

        //This method is called any time any of the variables in the slot is modified.
        private void OnSlotModified() 
        {
            if (!IsEmpty)
            {
                iconImage.sprite = slotItem.icon;
                countText.text = itemCount.ToString();
                iconImage.gameObject.SetActive(true);
            }
            else 
            {
                itemCount = 0;
                slotItem = null;
                iconImage.sprite = null;
                countText.text = string.Empty;
                iconImage.gameObject.SetActive(false);
            }
        }


        //Assigns the item and itemCount directly without any pre-checks.
        //NOTE: This should only be used for loading the container slot data.
        public void SetData(Item item, int count) 
        {
            slotItem = item;
            itemCount = count;
            OnSlotModified();
        }
    }
}

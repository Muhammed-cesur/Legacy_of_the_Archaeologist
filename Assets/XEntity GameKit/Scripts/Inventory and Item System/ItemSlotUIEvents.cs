using UnityEngine;
using UnityEngine.EventSystems;

namespace XEntity.InventoryItemSystem
{
    //This script is attached to the ItemSlot object; it handles the dragging/dropping/click events of the container slot UI.
    public class ItemSlotUIEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public static event System.Action OnSlotDrag;
        private static ItemSlot hoveredSlot; //The ItemSlot that is globally hovered over currently; null if none is hovered.
        private ItemSlot mySlot; //The ItemSlot object that this script is attached to.
        private UnityEngine.UI.Image slotUI;
        private Vector3 dragOffset;
        private Vector3 origin;
        private Color regularColor;
        private Color dragColor;
        private int originalSiblingIndex;

        public bool isBeingDragged { get; private set; } = false;

        private void Awake() 
        {
            //All the variables are initialized here.
            mySlot = GetComponent<ItemSlot>();
            slotUI = GetComponent<UnityEngine.UI.Image>();
            originalSiblingIndex = transform.GetSiblingIndex();

            origin = transform.localPosition;
            regularColor = slotUI.color;
            dragColor = new Color(regularColor.r, regularColor.g, regularColor.b, 0.3f);
        }

        private void Update()
        {
            if (isBeingDragged && hoveredSlot != null) 
            {
                if (Input.GetKeyDown(InteractionSettings.Current.transferSingleItem))
                {
                    Utils.TransferItemQuantity(mySlot, hoveredSlot, 1);
                }
                else if (Input.GetKeyDown(InteractionSettings.Current.transferHalfStack))
                {
                    Utils.TransferItemQuantity(mySlot, hoveredSlot, mySlot.itemCount / 2);
                }
            }
        }

        //This method is called when the mouse cursor enters this slot UI.
        public void OnPointerEnter(PointerEventData eventData)
        {
            hoveredSlot = mySlot;
        }

        //This method is called when the mouse cursor exists this slot UI.
        public void OnPointerExit(PointerEventData eventData)
        {
            hoveredSlot = null;
        }

        //This method is called when the mouse cursor starts dragging this slot UI.
        public void OnBeginDrag(PointerEventData eventData)
        {
            slotUI.transform.SetAsLastSibling();
            slotUI.color = dragColor;
            slotUI.raycastTarget = false;
            hoveredSlot = null;
            dragOffset = Input.mousePosition - transform.position;
            isBeingDragged = true;
        }

        //This method is continously called when the mouse cursor is dragging this slot UI.
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition - dragOffset;
            OnSlotDrag?.Invoke();
        }

        //This method is called when the mouse cursor stops dragging this slot UI.
        public void OnEndDrag(PointerEventData eventData)
        {
            //If there is a slot being hovered over, an attempt to transfer the items from this slot to the hovered slot will be made.
            if (hoveredSlot != null) OnDropToSlot();

            transform.SetSiblingIndex(originalSiblingIndex);
            transform.localPosition = origin;
            slotUI.color = regularColor;
            slotUI.raycastTarget = true;
            isBeingDragged = false;
        }

        //Tries to transfer the items from this slot to the hoveredSlot. 
        private void OnDropToSlot() 
        {
            Utils.TransferItem(mySlot, hoveredSlot);
        }
    } 
}

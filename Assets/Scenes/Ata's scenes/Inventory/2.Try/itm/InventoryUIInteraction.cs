using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryUIInteraction : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerExitHandler
{
    [SerializeField] GameObject ClickedItemUI;

    public Transform draggedItemParent;
    public Transform draggedItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
    
        if (transform.GetComponent<Slot>().ItemInSlot == null)
            return;
        print("Begin Drag");
        draggedItemParent = transform;
        draggedItem = draggedItemParent.GetComponentInChildren<RawImage>().transform;
        draggedItemParent.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        draggedItem.parent = FindObjectOfType<Canvas>().transform;
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        print("Dragging");
        draggedItem.position = Input.mousePosition;
        draggedItem.GetComponent<RawImage>().raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggedItem.parent = draggedItemParent;
        draggedItem.localPosition = new Vector3(0, 0, 0);
        draggedItem.GetComponent<RawImage>().raycastTarget = true;
        draggedItemParent.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        draggedItemParent.GetComponent<Slot>().SetStats();
        draggedItem = null;
        draggedItemParent = null;
        print("End Drag");

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerClick.GetComponent<Slot>().ItemInSlot == null || ClickedItemUI.activeInHierarchy)
            return;

        ClickedItemUI.transform.position = Input.mousePosition + new Vector3(ClickedItemUI.GetComponent<RectTransform>().rect.width * 1.5f / 2 +1,-(ClickedItemUI.GetComponent<RectTransform>().rect.height * 1.5f / 2 -1),0);
        ClickedItemUI.GetComponent<ClickedItem>().clickedSlot = eventData.pointerClick.GetComponent<Slot>();
        ClickedItemUI.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ClickedItemUI.SetActive(false);
    }

}

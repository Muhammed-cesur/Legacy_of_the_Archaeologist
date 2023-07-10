using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ItemInteraction : MonoBehaviour
{
    Transform cam;
    [SerializeField] LayerMask itemLayer;
    InventorySystem inventorySystem;

    [SerializeField] TextMeshProUGUI txt_HoveredItem;
    void Start()
    {
        cam = Camera.main.transform;
        inventorySystem = GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
        if(Physics.Raycast(cam.position,cam.forward, out hit,2, itemLayer))
        {
            if (!hit.collider.GetComponent<ItemObject>())
                return;



            txt_HoveredItem.text = $"Press 'F' to pick up {hit.collider.GetComponent<ItemObject>().amount}x {hit.collider.GetComponent<ItemObject>().itemStats.name}";

            if (Input.GetKeyDown(KeyCode.F))
            {
                inventorySystem.PickUpItem(hit.collider.GetComponent<ItemObject>());
            }
            

            
        }
        else
        {
            txt_HoveredItem.text = string.Empty;
        }
    }
}

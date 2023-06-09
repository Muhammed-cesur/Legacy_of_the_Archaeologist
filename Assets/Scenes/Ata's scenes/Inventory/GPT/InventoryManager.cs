using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public GameObject itemPrefab;
        public int quantity;
    }
    public GameObject healEffect;
    public GameObject powerEffect;
    public GameObject swordCode;
    public GameObject wall;
    public PlayerHealth playerHealth;
    public GameObject[] quickInventorySlots; // Array of GameObjects representing the slots in the quick inventory
    public List<InventoryItem> redItems; // List to store the red items in the inventory
    public List<InventoryItem> blueItems; // List to store the blue items in the inventory
    public List<InventoryItem> greenItems; // List to store the green items in the inventory
    public List<InventoryItem> otherItems; // List to store other items in the inventory

    public TMP_Text redItemsText; // Text component to display red items quantity
    public TMP_Text blueItemsText; // Text component to display blue items quantity
    public TMP_Text otherItemsText; // Text component to display other items quantity

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is a valid item that can be added to the inventory
        if (other.CompareTag("PickupItem") || other.CompareTag("Red") || other.CompareTag("Blue") || other.CompareTag("Green"))
        {
            CollectItem(other.gameObject);
        }
    }

    private void CollectItem(GameObject item)
    {
        InventoryItem newItem = new InventoryItem();
        newItem.itemPrefab = item;
        newItem.quantity = 1;

        // Determine the appropriate list to add the item based on its tag or other criteria
        if (item.CompareTag("Red"))
        {
            AddToInventory(newItem, redItems);
        }
        else if (item.CompareTag("Blue"))
        {
            AddToInventory(newItem, blueItems);
        }
        else if (item.CompareTag("Green"))
        {
            AddToInventory(newItem, greenItems);
        }
        else
        {
            AddToInventory(newItem, otherItems);
        }

        item.SetActive(false); // Disable the object so it's not visible in the game world
        UpdateInventoryUI();
    }

    private void AddToInventory(InventoryItem item, List<InventoryItem> inventoryList)
    {
        // Check if the item is already in the inventory
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            if (inventoryItem.itemPrefab == item.itemPrefab)
            {
                // Increase the quantity of the existing item in the inventory
                inventoryItem.quantity++;
                return;
            }
        }

        // If the item is not already in the inventory, add it as a new item
        inventoryList.Add(item);
    }

    private void UpdateInventoryUI()
    {
        // Update the UI elements to reflect the number of objects in each slot
        for (int i = 0; i < quickInventorySlots.Length; i++)
        {
            // Show the item in the slot if it exists, or hide it if the slot is empty
            if (i < redItems.Count)
            {
                quickInventorySlots[i].SetActive(true);
                Debug.Log((i + 1) + ". " + redItems[i].itemPrefab.name + " - Quantity: " + redItems[i].quantity);
            }
            else
            {
                quickInventorySlots[i].SetActive(false);
            }
        }

        // Update the text components for each inventory list
        redItemsText.text = GetCurrentItemAmount(redItems);
        blueItemsText.text = GetCurrentItemAmount(blueItems);
        otherItemsText.text = GetCurrentItemAmount(otherItems);
    }

    private string GetCurrentItemAmount(List<InventoryItem> inventoryList)
    {
        int totalQuantity = 0;

        // Calculate the total quantity of items in the inventory list
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            totalQuantity += inventoryItem.quantity;
        }

        // Return the total quantity as a string
        return totalQuantity.ToString();
    }


    private void Update()
    {
        // Check for key input to use the objects in the quick inventory
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (0 < redItems.Count)
            {
                // Heal the character
                playerHealth.TakeDamage(-50);
                Debug.Log("Player healed by 50 points.");
                StartCoroutine(EffectCooldown("heal"));
            }
            UseInventoryItem(0, redItems);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (0 < blueItems.Count)
            {
                swordCode.GetComponent<PlayerBlade>().PowerUp();
                StartCoroutine(EffectCooldown("powerUp"));
            }
            UseInventoryItem(0, blueItems);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseInventoryItem(0, greenItems);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (0 < otherItems.Count)
            {
                Destroy(wall);
            }
            UseInventoryItem(0, otherItems);

        }
        // Add more key checks as needed for additional slots
    }

    private IEnumerator EffectCooldown(string effect)
    {
        if (effect == "heal")
        {
            healEffect.SetActive(true);
            yield return new WaitForSeconds(1f);
            healEffect.SetActive(false);
        }
        else
        {
            powerEffect.SetActive(true);
            yield return new WaitForSeconds(20f);
            powerEffect.SetActive(false);
        }
    }

    private void UseInventoryItem(int slotIndex, List<InventoryItem> inventoryList)
    {
        // Check if the slot index is valid
        if (slotIndex >= 0 && slotIndex < inventoryList.Count)
        {
            InventoryItem item = inventoryList[slotIndex];
            // Implement the logic to use the item as desired
            Debug.Log("Using item: " + item.itemPrefab.name);
            item.quantity--;
            if (item.quantity <= 0)
            {
                inventoryList.RemoveAt(slotIndex);
            }
            UpdateInventoryUI();
        }
    }
}

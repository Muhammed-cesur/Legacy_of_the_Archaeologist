using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This script is attached to the item collector objects.
    //Item collectors are floating items prefabs in the scene that can be picked up by the interactor on collision.
    public class ItemCollector : MonoBehaviour
    {
        private Item item;

        //Continuously rotates the item collector along this axis.
        private readonly Vector3 rotAxis = new Vector3(0.1f, 1, 0.1f);

        private void Update() 
        {
            //Rotate the collector object.
            transform.Rotate(rotAxis, Time.deltaTime * 200);
        }

        //When the ItemCollector is attached to an object, this method should be called and the item this collector should be passed in.
        public void Create(Item item)
        {
            this.item = item;
        }

        //On trigger with the interactor, attempt will be made to add this collector's item to the interactor's inventory.
        private void OnTriggerEnter(Collider other) 
        {
            Interactor interactor = other.GetComponent<Interactor>();
            if (interactor != null) interactor.AddToInventory(item, gameObject);
        }
    } 
}

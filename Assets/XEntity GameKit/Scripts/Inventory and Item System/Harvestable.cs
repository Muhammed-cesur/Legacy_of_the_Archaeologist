using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This script is attached harvestable objects that are harvested based on HP such as trees.
    public class Harvestable : MonoBehaviour, IInteractable
    {
        //When this HP hits zero the object will be harvested.
        public float HP;

        //When this object is harvested, items based on these harvest drops will dropped for the player to pickup.
        public HarvestDrop[] harvestDrops;

        //This method is called when an interactor clicks on this object while being in range for an interaction.
        public void OnClickInteract(Interactor interactor)
        {
            HP--;
            if (HP <= 0) Harvest(interactor);
        }

        //This method is called when the HP is zero and item needs to be harvested.
        private void Harvest(Interactor interactor) 
        {
            System.Random prng = new System.Random(GetHashCode());

            //Items dropped based on their drop chance. 
            for (int i = 0; i < harvestDrops.Length; i++) 
            {
                HarvestDrop drop = harvestDrops[i];
                if (prng.NextDouble() <= drop.chance)
                    Utils.InstantiateItemCollector(drop.itemToDrop, transform.position + (transform.forward / 2));
            }                 

            //Onces all the harvested items are dropped, the harvestable object is destroyed.
            StartCoroutine(Utils.TweenScaleOut(gameObject, 40, true));
        }
    }


    //This struct contains the harvested item drop and the drop chance.
    [System.Serializable]
    public struct HarvestDrop
    {
        public Item itemToDrop;
        [Range(0, 1)]
        public float chance;
    }
}

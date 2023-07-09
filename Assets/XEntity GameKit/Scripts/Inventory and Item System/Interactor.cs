using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This class is attached to the player.
    //This holds the different types of interaction events and interaction trigger methods.
    public class Interactor : MonoBehaviour
    {
        //Reference to the main game viewing camera.
        [SerializeField] private Camera mainCamera;

        //Reference to the item container thats dedicated to this interactor.
        public ItemContainer inventory;

        //Reference to the current interactable target; always evaluated at runtime.
        //This is null if there are no valid target interactable objects. 
        private InteractionTarget interactionTarget;

        //This is the position at which dropped items will be instantiated (in front of this interactor).
        public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

        //Called every frame after the game is started.
        private void Update()
        {
            HandleInteractions();
        }

        //This method draws gizmos in the editor.
        private void OnDrawGizmos() 
        {
            if (InteractionSettings.Current.drawRangeIndicators) 
            {
                Gizmos.DrawWireSphere(transform.position, InteractionSettings.Current.interactionRange);
            }
        }

        //This method handles the interactable object detection, interaction trigger and the interaction event callbacks.
        private void HandleInteractions()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (interactionTarget?.gameObject != null) Utils.UnhighlightObject(interactionTarget.gameObject);

            if (Physics.Raycast(ray, out hit) && InRange(hit.transform.position))
            {
                IInteractable target = hit.transform.GetComponent<IInteractable>();
                if (target != null)
                {
                    interactionTarget = new InteractionTarget(target, hit.transform.gameObject);
                    Utils.HighlightObject(interactionTarget.gameObject);
                }
                else interactionTarget = null;
            }
            else
            {
                interactionTarget = null;
            }

            if (Input.GetMouseButtonDown(1)) InitInteraction(); // Changed from 0 to 1 for right mouse button
        }


        //This returns true if the target position is within the interaction range.
        private bool InRange(Vector3 targetPosition)
        {
            return Vector3.Distance(targetPosition, transform.position) <= InteractionSettings.Current.interactionRange;
        }

        //This method initilizes an interaction with this interactor if a valid interactabale target exists.
        private void InitInteraction() 
        {
            if (interactionTarget == null) return;
            interactionTarget.interactable.OnClickInteract(this);
        }

        //This method adds items to the inventory of this interactor and if applicable destroys the physical instance of the item.
        public bool AddToInventory(Item item, GameObject instance)
        {
            if (inventory.AddItem(item))
            {
                if (instance) Destroy(instance);
                return true;
            }
            return false;
        }

        internal class InteractionTarget 
        {
            internal IInteractable interactable;
            internal GameObject gameObject;
            public InteractionTarget(IInteractable interactable, GameObject gameObject) 
            {
                this.interactable = interactable;
                this.gameObject = gameObject;
            }
        }
    }
}

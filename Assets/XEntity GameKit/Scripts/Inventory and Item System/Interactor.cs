using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        public ItemContainer inventory;
        private InteractionTarget interactionTarget;

        public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

        private void Start()
        {
            // Attach collision events to the interactor's collider
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.isTrigger = true; // Set collider as a trigger to detect overlaps
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            IInteractable target = other.GetComponent<IInteractable>();
            if (target != null)
            {
                interactionTarget = new InteractionTarget(target, other.gameObject);
                Utils.HighlightObject(interactionTarget.gameObject);
                InitInteraction();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (interactionTarget?.gameObject == other.gameObject)
            {
                Utils.UnhighlightObject(interactionTarget.gameObject);
                interactionTarget = null;
            }
        }

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

            if (Input.GetMouseButtonDown(1)) InitInteraction();
        }

        private bool InRange(Vector3 targetPosition)
        {
            return Vector3.Distance(targetPosition, transform.position) <= InteractionSettings.Current.interactionRange;
        }

        private void InitInteraction()
        {
            if (interactionTarget == null) return;
            interactionTarget.interactable.OnClickInteract(this);
        }

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

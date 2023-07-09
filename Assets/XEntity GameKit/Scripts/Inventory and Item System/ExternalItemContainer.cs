using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    public class ExternalItemContainer : ItemContainer, IInteractable
    {
        [SerializeField] private bool toggleOnClick = true;
        [SerializeField, Space] private GameObject containerPrefab;
        [SerializeField] private Transform uiCanvas;

        protected override void InitializeContainer()
        {
            Transform containerPanelInstance = Instantiate(containerPrefab, uiCanvas).transform;

            //Destroy existing item container component on the prefab
            if (containerPanelInstance.TryGetComponent(out ItemContainer existingContainerComponent))
                Destroy(existingContainerComponent);

            IntialzieMainUI(containerPanelInstance);
            CreateSlotOptionsMenu(InteractionSettings.Current.externalSlotOptions, containerInteractor);
            isUIInitialized = true;
        }

        protected override void Update() 
        {
            if (isUIInitialized == false) return;

            if (containerInteractor != null)
            {
                //If current interactor is out range, set it to null.
                if (InteractionSettings.Current.autoCloseExternalContainer)
                {
                    float dist = Vector3.Distance(containerInteractor.transform.position, transform.position);
                    if (dist > InteractionSettings.Current.interactionRange)
                    {
                        containerInteractor = null;
                    }
                }
            }

            //Close container panel if there is no interaction in range.
            if (containerInteractor == null && isContainerUIOpen) 
            {
                ToggleUI();
            }
        }

        //This method draws gizmos in the editor.
        private void OnDrawGizmos() 
        {
            if (InteractionSettings.Current.drawRangeIndicators) 
            {
                Gizmos.DrawWireSphere(transform.position, InteractionSettings.Current.interactionRange);
            }
        }

        public void OnClickInteract(Interactor interactor)
        {
            if (!toggleOnClick) return;

            float dist = Vector3.Distance(interactor.transform.position, transform.position);
            if (dist <= InteractionSettings.Current.interactionRange)
            {
                containerInteractor = interactor;
                ToggleUI();
            }
        }
    }
}

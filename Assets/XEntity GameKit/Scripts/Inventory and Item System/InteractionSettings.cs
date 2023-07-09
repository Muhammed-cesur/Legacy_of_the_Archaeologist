using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    [CreateAssetMenu(fileName = "Interaction Settings", menuName = "XEntity/Interaction Settings")]
    public class InteractionSettings : ScriptableObject
    {
        //Singleton pattern
        private static InteractionSettings current;
        public static InteractionSettings Current
        {
            get
            {
                if (current == null) 
                {
                    InteractionSettings settings = Utils.FindInteractionSettings();
                    if (settings == null)
                    {
                        Debug.LogError("No interaction settings were found. Please create one.");
                    }
                    else current = settings;
                }
                return current;
            }
        }

        public ItemCollectorMode itemCollectorMode;
        public Color highlightColor;
        public float itemDropHeightOffset = 0.3f;
        public float interactionRange = 5;
        public bool autoCloseExternalContainer = true;
        public bool forceAddRequiredComponents = true;
        
        [Header("Slot Option Configuration")]
        public GameObject optionsMenuButtonPrefab;
        [Tooltip("Internal refers to players internals inventory while external refers to external storages (e.g. chests, loot boxes, etc).")]
        public SlotOptions[] internalSlotOptions;
        [Tooltip("Internal refers to players internals inventory while external refers to external storages (e.g. chests, loot boxes, etc).")]
        public SlotOptions[] externalSlotOptions;

        [Header("Input Preferences")]
        public KeyCode transferSingleItem = KeyCode.Mouse1;
        public KeyCode transferHalfStack = KeyCode.LeftShift;

        [Header("Editor Preferences")]
        public bool drawRangeIndicators = false;
    }

    [System.Serializable]
    public enum ItemCollectorMode
    {
        Static,
        PhysicsBody
    }
}

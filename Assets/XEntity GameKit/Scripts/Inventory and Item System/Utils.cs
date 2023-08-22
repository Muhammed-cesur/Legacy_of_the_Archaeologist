using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace XEntity.InventoryItemSystem
{
    //This struct contains utility functions for the inventory system, object tweening and object highlighting.
    public readonly struct Utils
    {
        /*
        * This method attempts to transfer the items from the trigger ItemSlot to the target ItemSlot.
        * If the item types are the same, they will stack until the maximum capcity is reached on the target ItemSlot.
        * If the item types are different they will swap places.
        */
        public static void TransferItem(ItemSlot trigger, ItemSlot target)
        {
            if (trigger == target) return;

            Item triggerItem = trigger.slotItem;
            Item targetItem = target.slotItem;

            int triggerItemCount = trigger.itemCount;

            if (!trigger.IsEmpty)
            {
                if (target.IsEmpty || targetItem == triggerItem)
                {
                    for (int i = 0; i < triggerItemCount; i++)
                    {
                        if (target.Add(triggerItem)) trigger.Remove(1);
                        else return;
                    }
                }
                else
                {
                    int targetItemCount = target.itemCount;

                    target.Clear();
                    for (int i = 0; i < triggerItemCount; i++) target.Add(triggerItem);

                    trigger.Clear();
                    for (int i = 0; i < targetItemCount; i++) trigger.Add(targetItem);
                }
            }
        }

        //This method attempts to transfer the passed in amount of items from the trigger ItemSlot to the target ItemSlot.
        public static void TransferItemQuantity(ItemSlot trigger, ItemSlot target, int amount) 
        {
            if (!trigger.IsEmpty) 
            {
                for (int i = 0; i < amount; i++) 
                {
                    if (!trigger.IsEmpty)
                    {
                        if (target.Add(trigger.slotItem)) trigger.Remove(1);
                        else return;
                    }
                    else return;
                }
            }
        }

        public static void TransferItemQuantity(ItemSlot trigger, ItemContainer targetContainer, int amount) 
        {
            if (!trigger.IsEmpty)
            {
                for (int i = 0; i < amount; i++)
                {
                    if (!trigger.IsEmpty)
                    {
                        if (targetContainer.AddItem(trigger.slotItem)) trigger.Remove(1);
                        else return;
                    }
                    else return;
                }
            }
        }

        /* 
         * This coroutine scales in the passed in obj from a scale of Vector3.zero to the passed in maxScale in the span of durationInFrames.
         * NOTE: This must be called from a MonoBehaviour script and must be called with StartCoroutine().
         * For example: StartCoroutine(GameUtility.TweenScaleIn(exampleGameObject, 40, Vector3.one));
        */
        public static IEnumerator TweenScaleIn(GameObject obj, float durationInFrames, Vector3 maxScale) 
        {
            Transform tf = obj.transform;
            tf.localScale = Vector3.zero;
            tf.gameObject.SetActive(true);

            float frame = 0;
            while (frame < durationInFrames) 
            {
                tf.localScale = Vector3.Lerp(Vector3.zero, maxScale, frame / durationInFrames);
                frame++;
                yield return null;
            }
        }

        /* 
         * This coroutine scales out the passed in obj from its original scale to a scale of Vector3.zero in the span of durationInFrames.
         * If destroy is true, the object will be destroyed after being scaled out.
         * If destroy is false, the object's active state is set to false after being scaled out.
         * NOTE: This must be called from a MonoBehaviour script and must be called with StartCoroutine().
         * For example: StartCoroutine(GameUtility.TweenScaleOut(exampleGameObject, 40, true));
        */
        public static IEnumerator TweenScaleOut(GameObject obj, float durationInFrames, bool destroy)
        {
            float frame = 0;
            while (frame < durationInFrames)
            {
                if (obj != null)
                {
                    obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, frame / durationInFrames);
                }
                frame++;
                yield return null;
            }
            if (obj)
            {
                if (!destroy) obj.SetActive(false);
                else GameObject.Destroy(obj);
            }
        }

        //Instantiates an item collector object with the passed in item at the passed in position.
        //An example is when an item is removed from the inventory, an item collector with the removed item is dropped in front of the player.
        public static void InstantiateItemCollector(Item item, Vector3 position) 
        {
            position.y += InteractionSettings.Current.itemDropHeightOffset;

            if (InteractionSettings.Current.itemCollectorMode == ItemCollectorMode.Static)
            {
                Vector3 targetSize = Vector3.one * 0.5f;
                GameObject instance = GameObject.Instantiate(item.prefab, position, Quaternion.identity);
                float maxSizeComponent = MaxVec3Component(instance.GetComponent<MeshRenderer>().bounds.size);

                instance.transform.localScale = instance.transform.localScale * (MaxVec3Component(targetSize) / maxSizeComponent);

                var interactable = instance.GetComponent<IInteractable>();
                if (interactable != null) GameObject.Destroy((Object)interactable);

                //init collider properties
                if (instance.TryGetComponent(out Collider col)) col.isTrigger = true;
                else
                {
                    if (InteractionSettings.Current.forceAddRequiredComponents)
                    {
                        MeshCollider _col = instance.AddComponent<MeshCollider>();
                        _col.convex = true;
                        _col.isTrigger = true;
                    }
                    else Debug.LogError($"[{item.name}] Item prefab does not have a Collider component.\nAll item prefabs must have a collider.");
                }

                instance.AddComponent<ItemCollector>().Create(item);
            }
            else if (InteractionSettings.Current.itemCollectorMode == ItemCollectorMode.PhysicsBody) 
            {
                GameObject instance = GameObject.Instantiate(item.prefab, position, Quaternion.identity);

                //init collider properties
                if (instance.TryGetComponent(out Collider col)) col.isTrigger = false;
                else 
                {
                    if (InteractionSettings.Current.forceAddRequiredComponents)
                    {
                        MeshCollider _col = instance.AddComponent<MeshCollider>();
                        _col.convex = true;
                        _col.isTrigger = false;
                    }
                    else Debug.LogError($"[{item.name}] Item prefab does not have a Collider component.\nAll item prefabs must have a collider."); 
                }

                //init rigidbody properties
                if (instance.TryGetComponent(out Rigidbody rb)) rb.useGravity = true;
                else
                {
                    if (InteractionSettings.Current.forceAddRequiredComponents)
                    {
                        Rigidbody _rb = instance.AddComponent<Rigidbody>();
                        _rb.useGravity = true;
                    }
                    else Debug.LogError($"[{item.name}] Item prefab does not have a Rigidbody component.\nPlease set collector type to static or add the required components."); 
                }
            }
        }

        //Returns the maximum of the three components of the passed in Vector3.
        public static float MaxVec3Component(Vector3 vec) 
        {
            return Mathf.Max(Mathf.Max(vec.x, vec.y), vec.z);
        }

        /*
         * Highlights the passed in obj with the passed in highlightColor.
         * NOTE: The object must have a mesh renderer with a valid material in order to be highlighted.
         */
        public static void HighlightObject(GameObject obj) 
        {
            obj.GetComponent<MeshRenderer>().material.color = InteractionSettings.Current.highlightColor;
        }


        /*
         * Unhighlights the passed in obj by setting the color to the original color.
         * NOTE: The object must have a mesh renderer with a valid material in order to be unhighlited.
         */
        public static void UnhighlightObject(GameObject obj, Color original) 
        {
            obj.GetComponent<MeshRenderer>().material.color = original;
        }

        /*
         * Unhighlights the passed in obj by setting the color to Color.white.
         * NOTE: The object must have a mesh renderer with a valid material in order to be unhighlited.
         */
        public static void UnhighlightObject(GameObject obj)
        {
            UnhighlightObject(obj, Color.white);
        }

        public static InteractionSettings FindInteractionSettings()
        {
            InteractionSettings settings = null;

            if (Application.isPlaying == false)
            {
#if UNITY_EDITOR
                var settingsAssets = AssetDatabase.FindAssets($"t:{nameof(InteractionSettings)}");
                if (settingsAssets?.Length > 0)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(settingsAssets[0]);
                    settings = AssetDatabase.LoadAssetAtPath(assetPath, typeof(InteractionSettings)) as InteractionSettings;
                }
#endif
            }
            else
            {
                if (ItemManager.Instance == null)
                {
                    settings = GameObject.FindObjectOfType<ItemManager>(true)?.interactionSettings;
                }
                else settings = ItemManager.Instance.interactionSettings;

                if (settings == null) 
                {
                    Debug.LogError("<color=red>No 'InteractionSettings' found. Must assign a 'InteractionSettings' to 'ItemManager'.</color>");
                }
            }

            return settings;
        }

    }
}

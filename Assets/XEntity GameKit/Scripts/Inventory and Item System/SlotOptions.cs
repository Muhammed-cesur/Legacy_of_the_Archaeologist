namespace XEntity.InventoryItemSystem 
{ 
    //Add custom slot options here
    [System.Serializable]
    public enum SlotOptions
    {
        Use,
        Remove,
        BulkRemove,
        ItemInfo,
        TransferToInventory
    }
}
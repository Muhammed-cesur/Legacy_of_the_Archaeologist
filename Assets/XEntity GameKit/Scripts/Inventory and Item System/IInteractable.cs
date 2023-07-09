namespace XEntity.InventoryItemSystem
{
    //The base class for all objects that the interactor can interact with.
    public interface IInteractable
    {
        //This method is called when the interactor attempts to interact with the object.
        //The passed in Interactor is the interactor that is attempting to interact.
        public void OnClickInteract(Interactor interactor);
    }
}

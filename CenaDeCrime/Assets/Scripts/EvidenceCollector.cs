using UnityEngine;

public class EvidenceCollector : Interactable
{
    [SerializeField] InventoryItem itemScript;
    protected override void Start()
    {
        if(GameManager.instance.inventoryController.IsItemAlreadyInInventory(itemScript.evidence))
        {
            Destroy(gameObject);
            return;
        }

        itemScript = GetComponent<InventoryItem>();
        base.Start();
        itemScript.enabled = false;
    }
    public override void Interact()
    {
        InventoryController inventoryController = GameManager.instance.inventoryController;
        if (inventoryController.isActivated) return;

        itemScript.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("InventoryLayer");
        inventoryController.AddItem(itemScript);
        this.enabled = false;
    }
    public override void OnRaycastEnter()
    {
        base.OnRaycastEnter();
    }
    public override void ChangeLayer(bool entered)
    {
        base.ChangeLayer(entered);
    }
    public override void OnRaycastExit()
    {
       base.OnRaycastExit();
    }
}

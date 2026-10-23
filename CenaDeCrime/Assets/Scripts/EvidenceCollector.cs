using UnityEngine;

public class EvidenceCollector : Interactable
{
    [SerializeField] InventoryItem itemScript;
    protected override void Start()
    {
        itemScript = GetComponent<InventoryItem>();
        base.Start();
        itemScript.enabled = false;
    }
    public override void Interact()
    {
        if(GameManager.instance.inventoryController.isActivated) return;
        itemScript.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("InventoryLayer");
        GameManager.instance.inventoryController.AddItem(itemScript);
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

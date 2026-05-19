using UnityEngine;

public class EvidenceCollector : Interactable
{
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        gameObject.layer = LayerMask.NameToLayer("InventoryLayer");
    }
    public override void OnRaycastEnter()
    {
        base.OnRaycastEnter();
    }
    public override void OnRaycastExit()
    {
       base.OnRaycastExit();
    }
}

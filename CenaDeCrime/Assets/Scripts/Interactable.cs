using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    [TextArea]
    [SerializeField] private string interactMessage = "Pressione <color=blue>E</color> para interagir";
    public GameManager gameManager;
    protected virtual void Start()
    {
        gameManager = GameManager.instance;
    }
    public virtual void Interact()
    {

    }
    public virtual void OnRaycastEnter()
    {
        gameManager.interactUI.GenerateText(interactMessage);
        ChangeLayer(true);
    }
    public virtual void ChangeLayer(bool entered)
    {
        string layer = entered ? "Interactable" : "InteractableWithoutOutline";
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
    public virtual void OnRaycastExit()
    {
        gameManager.interactUI.SetTextAtive(false);
        ChangeLayer(false);
    }
}

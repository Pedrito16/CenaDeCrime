using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Settings")]
    [TextArea]
    [SerializeField] private string interactMessage = "Pressione <color=blue>E</color> para interagir";
    [Header("Outline")]
    [SerializeField] bool useOutlineInChildrens = false;
    [SerializeField] MeshRenderer[] outlineRenderers;
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
        if (!useOutlineInChildrens)
        {
            string layer = entered ? "Interactable" : "InteractableWithoutOutline";
            gameObject.layer = LayerMask.NameToLayer(layer);
        }
        else
        {
            for(int i = 0; i < outlineRenderers.Length; i++)
            {
                string layer = entered ? "Interactable" : "InteractableWithoutOutline";
                outlineRenderers[i].gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }
    }
    public virtual void OnRaycastExit()
    {
        gameManager.interactUI.SetTextAtive(false);
        ChangeLayer(false);
    }
}

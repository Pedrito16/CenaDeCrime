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
    }
    public virtual void OnRaycastExit()
    {
        gameManager.interactUI.SetTextAtive(false);
    }
}

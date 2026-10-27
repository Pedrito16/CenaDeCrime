using UnityEngine;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [Header("Read-only")]
    public HorizontalInventory horizontalInventory;
    public PlayerMoviment playerMovement;
    public DialogueController dialogueController;
    public InteractUI interactUI;
    public ClipboardItem clipboardItem;
    public InventoryController inventoryController;
    public InteractRaycast interactRaycast;
    public InterrogationController interrogationController;
    public static GameManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {

    }
}

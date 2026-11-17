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
    public CheckBoxVerifier checkBoxVerifier;
    public static GameManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            print("deletando game manager da cena: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        print("player movement na cena: " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + " = " + playerMovement);
    }
    public void AutoDestroy()
    {
        instance = null;
        Destroy(gameObject);
    }
    public bool IsInventoryOpen()
    {
        return inventoryController != null && inventoryController.isActivated;
    }
}

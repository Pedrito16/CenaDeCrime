using UnityEngine;
[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    [Header("Read-only")]
    public PlayerMoviment playerMovement;
    public DialogueController dialogueController;
    public InteractUI interactUI;
    public InventoryController inventoryController;
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

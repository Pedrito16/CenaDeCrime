using UnityEngine;
using TMPro;
public class InteractUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI interactText;

    GameManager gameManager;
    public static InteractUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        gameManager = GameManager.instance;
        if(gameManager != null) gameManager.interactUI = this;

        interactText.gameObject.SetActive(false);
    }
    public void GenerateText(string message, Color textColor = default)
    {
        interactText.gameObject.SetActive(true);

        interactText.text = message;
        interactText.color = textColor == default ? Color.white : textColor;
    }
    public void SetTextAtive(bool active)
    {
        interactText.gameObject.SetActive(active);
    }
}

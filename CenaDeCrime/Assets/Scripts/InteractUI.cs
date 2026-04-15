using UnityEngine;
using TMPro;
public class InteractUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI interactText;

    GameManager gameManager;
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

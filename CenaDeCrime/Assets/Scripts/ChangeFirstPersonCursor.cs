using UnityEngine;
using UnityEngine.UI;
public class ChangeFirstPersonCursor : MonoBehaviour
{
    [SerializeField] Image cursorImage;

    [Header("Cursor Sprites")]
    [SerializeField] Sprite originalCursorSprite;
    [SerializeField] Sprite evidenceCursorSprite;

    [Header("Debug")]
    [SerializeField] bool isEvidenceCursorActive = false;

    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.interactRaycast.onEvidenceEnterExit.AddListener(OnEvidenceEnter);
    }
    void OnEvidenceEnter()
    {
        if(!isEvidenceCursorActive)         
        {
            cursorImage.sprite = evidenceCursorSprite;
            isEvidenceCursorActive = true;
        }
        else
        {
            cursorImage.sprite = originalCursorSprite;
            isEvidenceCursorActive = false;
        }
    }
    void Update()
    {
        
    }
}

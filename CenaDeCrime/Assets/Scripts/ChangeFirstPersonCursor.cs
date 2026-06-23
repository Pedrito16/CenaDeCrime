using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class FPSCursor
{
    public Sprite spriteCursor;
    public Vector2 cursorSize = Vector2.one;
}
public class ChangeFirstPersonCursor : MonoBehaviour
{
    [SerializeField] Image cursorImage;

    [Header("Cursor Sprites")]
    [SerializeField] FPSCursor originalCursor;
    [SerializeField] FPSCursor evidenceCursor;

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
            ChangeCursor(evidenceCursor);
            isEvidenceCursorActive = true;
        }
        else
        {
            ChangeCursor(originalCursor);
            isEvidenceCursorActive = false;
        }
    }
    void ChangeCursor(FPSCursor newCursor)
    {
        cursorImage.sprite = newCursor.spriteCursor;
        cursorImage.rectTransform.sizeDelta = newCursor.cursorSize;
    }
    void Update()
    {
        
    }
}

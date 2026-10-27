using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PranchetaMoveDown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform prancheta;
    [SerializeField] GameObject upArrow;
    [SerializeField] GameObject downArrow;
    [Space(10)]
    [SerializeField] float scrollValue;
    [Space(10)]
    [SerializeField] float upPos;
    [SerializeField] float downPos;
    void Start()
    {
        
    }
    public void MoveDown()
    {
        prancheta.DOAnchorPosY(downPos, 0.5f);
        upArrow.SetActive(true);
        downArrow.SetActive(false);
    }
    public void MoveUp()
    {
        prancheta.DOAnchorPosY(upPos, 0.5f);
        upArrow.SetActive(false);
        downArrow.SetActive(true);
    }
    public void OnScroll(InputAction.CallbackContext context)
    {
        if(!GameManager.instance.clipboardItem.clipboardUI.activeSelf)
            return;

        scrollValue = context.ReadValue<float>();
        scrollValue = Mathf.Clamp(scrollValue, -1, 1);
        if(scrollValue > 0)
            MoveUp();
        else if(scrollValue < 0)
            MoveDown();
    }
}

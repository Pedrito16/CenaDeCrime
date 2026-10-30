using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[System.Serializable]
public class CanvasGroupAlpha
{
    public float originalAlpha;
    public CanvasGroup canvasGroup;
}
public class ClipboardItem : Items
{
    [Header("References")]
    [SerializeField] FollowObject clipboardFollow;
    public GameObject clipboardUI;
    [SerializeField] CanvasGroupAlpha[] clipboardGroups;

    [Header("Config")]
    [SerializeField] float duration = 0.5f;
    void Start()
    {
        GameManager.instance.clipboardItem = this;
        gameObject.SetActive(false);
        clipboardUI.SetActive(false);
    }
    public override void UseItem()
    {
        clipboardUI.SetActive(true);
        clipboardFollow.SwitchTarget(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        for(int i = 0; i < clipboardGroups.Length; i++)
        {
            clipboardGroups[i].canvasGroup.alpha = 0f;
            clipboardGroups[i].canvasGroup.DOFade(clipboardGroups[i].originalAlpha, duration);
        }
        GameManager.instance.playerMovement.canMove = false;
        GameManager.instance.playerMovement.canMoveCam = false;
    }
    public override void StopUsingItem()
    {
        clipboardFollow.SwitchTarget(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        for (int i = 0; i < clipboardGroups.Length; i++)
        {
            clipboardGroups[i].canvasGroup.DOFade(0f, duration);
        }
        clipboardUI.SetActive(false);
        GameManager.instance.playerMovement.canMove = true;
        GameManager.instance.playerMovement.canMoveCam = true;
    }
}
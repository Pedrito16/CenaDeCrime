using UnityEngine;
using UnityEngine.UI;
public class ClipboardItem : Items
{
    [SerializeField] FollowObject clipboardFollow;
    [SerializeField] CanvasGroup clipboardUI;
    void Start()
    {
        
    }
    public override void UseItem()
    {
        clipboardFollow.SwitchTarget(true);
    }
    public override void StopUsingItem()
    {
        clipboardFollow.SwitchTarget(false);
    }
}
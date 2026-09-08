using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class ItemPanelController : MonoBehaviour
{
    [SerializeField] RectTransform itemPanel;
    [SerializeField] float animDuration = 2f;
    [SerializeField] bool isActive = false;
    Vector2 itemPanelPosDeactivated;
    Vector2 originalPos;
    void Start()
    {
        originalPos = itemPanel.anchoredPosition;
        itemPanelPosDeactivated = new Vector2(itemPanel.anchoredPosition.x, itemPanel.anchoredPosition.y - 50f);
    }
    void ActivateItemPanel(bool activate)
    {
        StartCoroutine(MoveRoutine(activate));
    }
    IEnumerator MoveRoutine(bool activate)
    {
        float t = 0;
        Vector2 targetPos = activate ? originalPos : itemPanelPosDeactivated;
        isActive = activate;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            itemPanel.anchoredPosition = Vector2.Lerp(itemPanel.anchoredPosition, targetPos, t);
            yield return null;
        }
    }
    public void OnEquipItemIndex(InputAction.CallbackContext context)
    {

    }
}

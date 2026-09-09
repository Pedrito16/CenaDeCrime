using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class ItemPanelController : MonoBehaviour
{
    [SerializeField] RectTransform itemPanel;
    [SerializeField] float animDuration = 2f;
    [SerializeField] bool isActive = false;
    [SerializeField] float timeActivated = 5f;
    float timer;
    Vector2 itemPanelPosDeactivated;
    Vector2 originalPos;
    void Start()
    {
        originalPos = itemPanel.anchoredPosition;
        itemPanelPosDeactivated = new Vector2(itemPanel.anchoredPosition.x, itemPanel.anchoredPosition.y - 125);
        ActivateItemPanel(isActive);
    }
    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= timeActivated)
            {
                ActivateItemPanel(false);
                timer = 0f;
            }
        }
    }
    void ActivateItemPanel(bool activate)
    {
        StartCoroutine(MoveRoutine(activate));
    }
    IEnumerator MoveRoutine(bool activate)
    {
        float t = 0;
        Vector2 startPos = itemPanel.anchoredPosition;
        Vector2 targetPos = activate ? originalPos : itemPanelPosDeactivated;
        isActive = activate;
        while (t < animDuration)
        {
            t += Time.deltaTime;
            itemPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, t / animDuration);
            yield return null;
        }
    }
    public void OnEquipItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isActive)
                ActivateItemPanel(true);
            else
                timer = 0;

        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class InterrogationSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] int index;

    [Header("Debug")]
    [SerializeField] InterrogationController interrogationController;
    void Start()
    {
        interrogationController = GameManager.instance.interrogationController;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        interrogationController.OnChangeSelected(index);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        interrogationController.lastSuspectIndex = index;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        interrogationController.OnSelectSuspect();
    }
}

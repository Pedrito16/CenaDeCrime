using UnityEngine;
using UnityEngine.EventSystems;
public class MoveInventoryArrows : MonoBehaviour, IPointerClickHandler
{
    [Range(-1, 1)]
    [SerializeField] private int direction;
    void Start()
    {

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.inventoryController.Move(direction);
    }
}

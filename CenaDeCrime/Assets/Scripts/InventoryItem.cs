using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{ 
    void Start()
    {
        
    }
    public void MoveToNext(int direction, float amountXToMove, bool isSelected)
    {
        Vector3 newPos = new Vector3(transform.localPosition.x + (direction * amountXToMove), transform.localPosition.y, isSelected ? -1 : 0);
        transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.OutCubic);
    }
}

using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{ 
    void Start()
    {
        
    }
    public void MoveToNext(int direction, float amountXToMove)
    {
        Vector3 newPos = new Vector3(transform.localPosition.x + (direction * amountXToMove), transform.localPosition.y, transform.localPosition.z);
        transform.DOLocalMove(newPos, 0.5f);
    }
}

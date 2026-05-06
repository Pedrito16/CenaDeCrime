using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{
    Tween moveTween;
    Vector3 lastPos;
    void Start()
    {
        
    }
    public void MoveToNext(int direction, float amountXToMove, bool isSelected)
    {
        if(moveTween != null) EndTween();
        moveTween = null;
        Vector3 newPos = new Vector3(transform.localPosition.x + (direction * amountXToMove), transform.localPosition.y, isSelected ? -1 : 0);
        lastPos = newPos;
        moveTween = transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.OutCubic);
    }
    void EndTween()
    {
        print("end tween");
        transform.localPosition = lastPos;
        moveTween.Kill();
    }
}

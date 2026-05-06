using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{
    Tween moveTween;
    Vector3 lastPos;
    Vector3 originalPos;
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
    public void MoveDownwards()
    {
        originalPos = transform.localPosition;

        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 10, transform.localPosition.z);
        moveTween = transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InCubic).OnComplete(() => SetActive(false));
    }
    void EndTween()
    {
        transform.localPosition = lastPos;
        moveTween.Kill();
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

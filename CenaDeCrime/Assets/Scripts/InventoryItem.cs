using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{
    public EvidenceInfoSO evidence;
    Tween moveTween;
    Vector3 lastPos;
    void Start()
    {
        
    }
    public void MoveToNext(int direction, float amountXToMove, bool isSelected)
    {
        if (direction == 0) 
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, isSelected ? -1 : 0);
            return;
        } 
        if(moveTween != null) EndTween();
        moveTween = null;

        Vector3 newPos = new Vector3(transform.localPosition.x + (direction * amountXToMove), transform.localPosition.y, isSelected ? -1 : 0);
        lastPos = newPos;

        moveTween = transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.OutCubic);
    }
    public void ResetRotation()
    {
        transform.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.OutCubic);
    }
    public void MoveDownwards(bool downWards = true)
    {
        SetActive(true);

        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y + (downWards ? -5 : 5), transform.localPosition.z);
        moveTween = transform.DOLocalMove(newPos, 0.5f).SetEase(Ease.InCubic).OnComplete(() => SetActive(!downWards));
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

using UnityEngine;
using DG.Tweening;

public class InventoryItem : MonoBehaviour
{
    public EvidenceInfoSO evidence;
    Tween moveTween;
    Tween returnTween;
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

        if (returnTween != null) 
        {
            EndTween(returnTween);
            SetActive(true);
            print("matei tween");
            returnTween = null;
        }

        if (moveTween != null) 
        {
            EndTween(moveTween);
            moveTween = null;
        }
        

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
        if (downWards)
        {
            if(returnTween != null) 
            {
                EndTween(returnTween);
                SetActive(true);
                returnTween = null;
                return;
            }
            lastPos = transform.localPosition;
        }

        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y + (downWards ? -5 : 5), transform.localPosition.z);
        returnTween = transform.DOLocalMove(newPos, 0.25f).SetEase(Ease.InCubic).OnComplete(() => SetActive(!downWards));
    }
    void EndTween(Tween tweenToEnd)
    {
        transform.localPosition = lastPos;
        tweenToEnd.Kill();
        
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

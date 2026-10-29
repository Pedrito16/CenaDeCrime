using UnityEngine;
using DG.Tweening;
public class MoveTween : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Ease ease;
    [SerializeField] Ease outEase;
    [SerializeField] Vector3 originalPos;
    [SerializeField] Vector3 target;
    [SerializeField] bool useRectTransform;
    [SerializeField] bool useOriginalPosAsTarget;
    void Start()
    {
        RectTransform rectTransform = null;
        if(useRectTransform) rectTransform = GetComponent<RectTransform>();
        if (useOriginalPosAsTarget)
        {
            target = useRectTransform ? rectTransform.anchoredPosition : transform.localPosition;
            rectTransform.anchoredPosition = originalPos;
        }
    }
    public void MoveToTarget()
    {
        if (useRectTransform)
        {
            if (!useOriginalPosAsTarget)
                GetComponent<RectTransform>().DOAnchorPos(originalPos, duration).SetEase(ease);
            else
                GetComponent<RectTransform>().DOAnchorPos(target, duration).SetEase(ease);
        }
        else
        {
            transform.DOLocalMove(target, duration).SetEase(ease);
        }
    }
    public void MoveToOriginal()
    {
        if (useRectTransform)
        {
            if (!useOriginalPosAsTarget)
                GetComponent<RectTransform>().DOAnchorPos(target, duration / 2).SetEase(ease);
            else
                GetComponent<RectTransform>().DOAnchorPos(originalPos, duration / 2).SetEase(outEase);
        }
        else
        {
            transform.DOLocalMove(originalPos, duration).SetEase(ease);
        }
    }
}

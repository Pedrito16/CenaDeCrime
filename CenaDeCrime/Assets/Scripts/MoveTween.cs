using UnityEngine;
using DG.Tweening;
public class MoveTween : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] Ease ease;
    [SerializeField] Vector3 originalPos;
    [SerializeField] Vector3 target;
    void Start()
    {
        
    }
    public void MoveToTarget()
    {
        print("movendo");
        transform.DOLocalMove(target, duration).SetEase(ease);
    }
    public void MoveToOriginal()
    {
        transform.DOLocalMove(originalPos, duration).SetEase(ease);
    }
}

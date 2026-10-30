using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class FadeController : MonoBehaviour
{
    
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeDuration;
    [SerializeField] bool fadeOnStart = true;
    [SerializeField] UnityEvent onFadeOutComplete;
    void Start()
    {
        if (fadeOnStart)
        {
            FadeIn();
        }
    }
    public void FadeIn()
    {
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 1;
            fadeGroup.DOFade(0, fadeDuration);
        }
    }
    public void FadeOut()
    {
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 0;
            fadeGroup.DOFade(1, fadeDuration).OnComplete(() =>
            {
                onFadeOutComplete?.Invoke();
            });
        }
    }
}

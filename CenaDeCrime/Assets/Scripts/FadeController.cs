using UnityEngine;
using DG.Tweening;
public class FadeController : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] float fadeDuration;
    void Start()
    {
        FadeIn();
    }
    public void FadeIn()
    {
        if (fadeGroup != null)
        {
            fadeGroup.alpha = 1;
            fadeGroup.DOFade(0, fadeDuration);
        }
    }
}

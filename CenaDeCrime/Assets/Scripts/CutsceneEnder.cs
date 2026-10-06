using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CutsceneEnder : MonoBehaviour
{
    [Header("References")]
    [SerializeField] CutsceneDialogue cutsceneDialogue;
    [SerializeField] CanvasGroup fadeImage;
    [SerializeField] string sceneToLoad;

    [Header("Settings")]
    [SerializeField] float fadeDuration;
    void Start()
    {
        fadeImage.alpha = 0;
    }
    public void OnTimelineEnd()
    {
        if(cutsceneDialogue != null && cutsceneDialogue.endedDialogues)
        {
            fadeImage.DOFade(1, fadeDuration).OnComplete(() => SceneManager.LoadScene(sceneToLoad));
        }
    }
}

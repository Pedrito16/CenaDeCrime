using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//using eu ando nas ruas;

[System.Serializable]
public class Suspect
{
    public GameObject lightSource;
    public Transform frontFaceCam;
    public string whatWillSay;
}
public class InterrogationController : MonoBehaviour
{
    [SerializeField] public Suspect[] suspects;
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] TMP_Text text;
    [SerializeField] PassScene passSceneScript;

    [Header("Config")]
    [SerializeField] float typeWritterSpeed = 0.05f;

    [Header("Debug")]
    [SerializeField] UnityEvent onConfirmSuspect;
    [SerializeField] UnityEvent onSelectSuspect;
    [SerializeField] UnityEvent onDeselectSuspect;
    [SerializeField] int currentSuspectIndex;
    public int lastSuspectIndex;
    [Space(10)]
    [SerializeField] bool isSelecting = false;
    [SerializeField] bool canChangeSelected = true;
    [SerializeField] bool isAnimationComplete = true;

    //privates
    MenuCameraFollow camScript;
    Vector3 camOriginalPos;
    Vector3 camOriginalRotation;
    void Awake()
    {
        GameManager.instance.interrogationController = this;
        DeactivateAllLights();

        camScript = mainCameraTransform.GetComponent<MenuCameraFollow>();
        camOriginalPos = mainCameraTransform.position;
        camOriginalRotation = mainCameraTransform.rotation.eulerAngles;
    }
    void DeactivateAllLights()
    {
        for (int i = 0; i < suspects.Length; i++)
        {
            suspects[i].lightSource.SetActive(false);
        }
    }
    public void OnChangeSelected(int newSuspect)
    {
        if (!canChangeSelected) return;

        suspects[lastSuspectIndex].lightSource.SetActive(false);
        suspects[newSuspect].lightSource.SetActive(true);
        currentSuspectIndex = newSuspect;
    }
    public void OnConfirmSuspect()
    {
        if (!isAnimationComplete) return;
        passSceneScript.sceneIndex = currentSuspectIndex;
        onConfirmSuspect?.Invoke();
    }
    IEnumerator TypeWritter(string textToWrite)
    {
        text.gameObject.SetActive(true);
        char[] chars = textToWrite.ToCharArray();
        for(int i = 0;i < chars.Length; i++)
        {
            text.text += chars[i];
            yield return new WaitForSeconds(typeWritterSpeed);
        }
    }
    #region Select
    public void OnSelectSuspect()
    {
        if (!isAnimationComplete || isSelecting) return;

        Suspect currentSuspect = suspects[currentSuspectIndex];
        isSelecting = true;
        StartCoroutine(TypeWritter(currentSuspect.whatWillSay));

        canChangeSelected = false;
        isAnimationComplete = false;
        camScript.enabled = false;

        onSelectSuspect?.Invoke();

        mainCameraTransform.DOMove(currentSuspect.frontFaceCam.position, 0.5f).OnComplete(() =>
        {
            SwitchCams(true);
            isAnimationComplete = true;
        });

        mainCameraTransform.DORotateQuaternion(currentSuspect.frontFaceCam.rotation, 0.5f); 
    }
    void OnDeselectSuspect()
    {
        Suspect currentSuspect = suspects[currentSuspectIndex];
        StopCoroutine(TypeWritter(currentSuspect.whatWillSay));
        text.gameObject.SetActive(false);
        isSelecting = false;
        text.text = "";

        isAnimationComplete = false;
        SwitchCams(false);

        onDeselectSuspect?.Invoke();

        mainCameraTransform.DOMove(camOriginalPos, 0.5f).OnComplete(() => 
        {
            camScript.enabled = true;
            canChangeSelected = true;
            isAnimationComplete = true;
        });

        mainCameraTransform.DORotate(camOriginalRotation, 0.5f);
    }
    void SwitchCams(bool isGoing = true)
    {
        Suspect currentSuspect = suspects[currentSuspectIndex];

        mainCameraTransform.gameObject.SetActive(!isGoing);
        currentSuspect.frontFaceCam.gameObject.SetActive(isGoing);
    }
    #endregion
    #region Inputs
    public void OnDeselectSuspect(InputAction.CallbackContext context)
    {
        if (context.started && !canChangeSelected && isAnimationComplete) //se eu nao puder trocar, estou selecionando
            OnDeselectSuspect();
    }
    #endregion
}

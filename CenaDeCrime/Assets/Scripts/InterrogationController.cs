using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
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

    [Header("Debug")]
    [SerializeField] int currentSuspectIndex;
    public int lastSuspectIndex;
    [Space(10)]
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
    #region Select
    public void OnSelectSuspect()
    {
        if (!isAnimationComplete) return;

        Suspect currentSuspect = suspects[currentSuspectIndex];

        canChangeSelected = false;
        isAnimationComplete = false;
        camScript.enabled = false;

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

        isAnimationComplete = false;
        SwitchCams(false);

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

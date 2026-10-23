using UnityEngine;
using DG.Tweening;
public class Interruptor : Interactable
{
    [Header("Interruptor")]
    [SerializeField] GameObject lightSource;
    [SerializeField] Transform buttonTransform;
    [SerializeField] AudioSource toggleSound;
    [SerializeField] bool isOn = false;
    [SerializeField] GameObject[] objectsToActivate;
    [Header("Button Pos")]
    [SerializeField] Vector3 buttonOnPos;
    [SerializeField] Vector3 buttonOffPos;
    protected override void Start()
    {
        base.Start();
    }
    public override void OnRaycastEnter()
    {
        base.OnRaycastEnter();
    }
    public override void OnRaycastExit()
    {
        base.OnRaycastExit();
    }
    public override void Interact()
    {
        isOn = !isOn;
        lightSource.SetActive(isOn);
        buttonTransform.DOLocalMove(isOn ? buttonOnPos : buttonOffPos, 0.2f);

        ToggleActivates();

        if(toggleSound != null)
            toggleSound.Play();
    }
    void ToggleActivates()
    {
        for(int i = 0; i < objectsToActivate.Length; i++)
        {
            objectsToActivate[i].SetActive(!objectsToActivate[i].activeSelf);
        }
    }
    public override void ChangeLayer(bool entered)
    {
        base.ChangeLayer(entered);
    }
}

using UnityEngine;
using DG.Tweening;
public class Interruptor : Interactable
{
    [SerializeField] GameObject lightSource;
    [SerializeField] Transform buttonTransform;
    [SerializeField] AudioSource toggleSound;
    [SerializeField] bool isOn = false;
    [Header("Button Pos")]
    [SerializeField] Vector3 buttonOnPos;
    [SerializeField] Vector3 buttonOffPos;
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        isOn = !isOn;
        lightSource.SetActive(isOn);
        buttonTransform.DOLocalMove(isOn ? buttonOnPos : buttonOffPos, 0.2f);
        if(toggleSound != null)
            toggleSound.Play();
    }
    public override void ChangeLayer(bool entered)
    {
        base.ChangeLayer(entered);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterInteract : Interactable
{
    [SerializeField] private string sceneName;
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        SceneManager.LoadScene(sceneName);
        OnRaycastExit();
    }
    public override void OnRaycastEnter()
    {
        base.OnRaycastEnter();
    }
    public override void OnRaycastExit()
    {
        base.OnRaycastExit();
    }
}

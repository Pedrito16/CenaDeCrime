using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnterInteract : Interactable
{
    [SerializeField] private string sceneName;
    [SerializeField] private Vector3 playerPosOnNewScene;
    protected override void Start()
    {
        base.Start();
    }
    public override void Interact()
    {
        gameManager.interactUI.SetTextAtive(false);
        StartCoroutine(OneSecond());
    }
    IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);

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

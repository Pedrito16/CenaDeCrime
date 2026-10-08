using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] MonitorInteractive[] monitorButtons;
    [SerializeField] int currentClicked;
    [SerializeField] string sceneName; 
    public static MenuController instance;
    void Start()
    {
        if (instance == null) instance = this;
    }
    public void OnClickButton(int index)
    {
        for(int i = 0; i < monitorButtons.Length; i++)
        {
            if (i == index) continue;

            monitorButtons[i].enabled = false;
        }
    }
    public void ActivateButtons()
    {
        for (int i = 0; i < monitorButtons.Length; i++)
        {
            monitorButtons[i].enabled = true;
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
    {
        print("Quit");
        Application.Quit();
    }
}

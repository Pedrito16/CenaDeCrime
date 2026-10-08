using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] MonitorInteractive[] monitorButtons;
    [SerializeField] int currentClicked;
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
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryAmountUI : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballContainer;
    [SerializeField] List<CanvasGroup> ballsSelection;
    private void Awake()
    {

    }
    void Start()
    {
        
    }
    public void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballContainer);
        CanvasGroup canvasGroup = ball.GetComponent<CanvasGroup>();
        if(canvasGroup != null)
            ballsSelection.Add(canvasGroup);

        canvasGroup.alpha = 0.5f;
    }
    public void SelectBall(int index)
    {
        for(int i = 0; i < ballsSelection.Count; i++)
        {
            if(i == index)
                ballsSelection[i].alpha = 1f;
            else
                ballsSelection[i].alpha = 0.5f;
        }
    }
}

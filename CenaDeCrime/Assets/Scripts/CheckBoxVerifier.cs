using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class CheckBoxVerifier : MonoBehaviour
{
    [SerializeField] CheckBoxChecker[] checkBoxCheckers;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button interrogatoryButton;
    [SerializeField] string sceneToLoad;
    Color originalTextColor;
    public static CheckBoxVerifier instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameManager.instance.checkBoxVerifier = this;
    }
    private void Start()
    {
        originalTextColor = buttonText.color;
        DidntAnsweredAll();
    }
    public void CheckIfAnsweredAll()
    {
        for(int i = 0; i < checkBoxCheckers.Length; i++)
        {
            if(checkBoxCheckers[i].AnsweredCheckBox())
                continue;
            else
            {
                DidntAnsweredAll();
                print("nao respondeu todas");
                return;      
            }
        }
        print("respondeu todas");
        buttonText.color = originalTextColor;
        interrogatoryButton.interactable = true;
    }
    public void OnComplete()
    {
        GameManager.instance.AutoDestroy();
        SceneManager.LoadScene(sceneToLoad);
    }
    void DidntAnsweredAll()
    {
        interrogatoryButton.interactable = false;
        buttonText.color = Color.gray;
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CheckBoxVerifier : MonoBehaviour
{
    [SerializeField] CheckBoxChecker[] checkBoxCheckers;
    [SerializeField] Button interrogatoryButton;
    [SerializeField] string sceneToLoad;
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
    public void CheckIfAnsweredAll()
    {
        for(int i = 0; i < checkBoxCheckers.Length; i++)
        {
            if(checkBoxCheckers[i].AnsweredCheckBox())
                continue;
            else
            {
                DidntAnsweredAll();
                return;      
            }
        }
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
    }
}

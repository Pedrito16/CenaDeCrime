using UnityEngine;
using UnityEngine.SceneManagement;

public class PassScene : MonoBehaviour
{
    public int sceneIndex;
    [SerializeField] private string[] scenes;
    [SerializeField] private string sceneName;
    [SerializeField] bool useArray = false;
    void Start()
    {
        
    }
    public void ChangeScene()
    {
        if (useArray)
        {
            SceneManager.LoadScene(scenes[sceneIndex]);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }   
    }
}

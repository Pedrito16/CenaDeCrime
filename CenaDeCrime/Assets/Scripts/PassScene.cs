using UnityEngine;
using UnityEngine.SceneManagement;

public class PassScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    void Start()
    {
        
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}

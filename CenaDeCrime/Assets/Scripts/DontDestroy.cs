using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            print("deletando UI da cena: " + SceneManager.GetActiveScene().name);
            Destroy(gameObject);
            return;
        }
        
        print("mantendo UI da cena: " + SceneManager.GetActiveScene().name);
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

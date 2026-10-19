using System.Threading;
using UnityEngine;
[System.Serializable]
public class SirenComponents
{
    public GameObject offObject;
    public GameObject onObject;
    public GameObject sirenLight;
    public bool isActive;
}

public class PoliceSiren : MonoBehaviour
{
    [SerializeField] SirenComponents[] sirens;
    [SerializeField] float frequency = 1.5f;
    [SerializeField] int currentIndex = 0;
    float timer = 0f;
    void Start()
    {
        for(int i = 0; i < sirens.Length; i++)
        {
            sirens[i].sirenLight.SetActive(false);
            sirens[i].offObject.SetActive(true);
            sirens[i].onObject.SetActive(false);
            sirens[i].isActive = false;
        }
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= frequency)
        {
            DeactiveOther();
            ActivateOther();
            timer = 0f;
        }
    }
    void DeactiveOther()
    {
        SirenComponents currentSiren = sirens[currentIndex];
        currentSiren.isActive = false;
        currentSiren.sirenLight.SetActive(false);
        currentSiren.offObject.SetActive(true);
        currentSiren.onObject.SetActive(false);
    }
    void ActivateOther()
    {
        currentIndex++;
        if(currentIndex >= sirens.Length)
        {
            currentIndex = 0;
        }
        SirenComponents currentSiren = sirens[currentIndex];
        currentSiren.isActive = true;
        currentSiren.sirenLight.SetActive(true);
        currentSiren.offObject.SetActive(false);
        currentSiren.onObject.SetActive(true);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer master;
    void Start()
    {
        
    }
    public void OnChangeVolume()
    {
        master.SetFloat("MasterVolume", Mathf.Log10(volumeSlider.value) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volumeSlider.value);
    }
}

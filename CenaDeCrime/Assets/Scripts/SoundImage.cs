using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class SoundImage : MonoBehaviour
{
    [SerializeField] Material[] paperMaterials;
    [SerializeField] MeshRenderer paperMeshRenderer;
    [SerializeField] float valueThreshold = 0.25f;
    [SerializeField] Slider volumeSlider;
    void Start()
    {

    }
    public void OnChangeVolume()
    {
        float volumeValue = volumeSlider.value;
        for (int i = 0; i < paperMaterials.Length; i++)
        {
            if(volumeValue >= valueThreshold * i && volumeValue < valueThreshold * (i + 1))
            {
                paperMeshRenderer.material = paperMaterials[i];
            }
        }
    }
}

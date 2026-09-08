using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HorizontalInventoryItem
{
    public Image itemSelectedImage;
    public GameObject itemObject;
}
public class HorizontalInventory : MonoBehaviour
{
    [SerializeField] HorizontalInventoryItem[] items;
    void Start()
    {
        
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CheckBox : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] Image boxImage;
    [SerializeField] Sprite emptyBox;
    [SerializeField] Sprite checkedBox;
    [Space(10)]
    public bool isChecked = false;
    void Start()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isChecked = !isChecked;
        boxImage.sprite = isChecked ? checkedBox : emptyBox;
    }
}

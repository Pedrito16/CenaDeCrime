using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CheckBox : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] Image boxImage;
    [SerializeField] Sprite emptyBox;
    [SerializeField] Sprite checkedBox;
    [SerializeField] UnityEvent onChecked;
    [Space(10)]
    public bool isChecked = false;
    void Start()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager.checkBoxVerifier != null)
            onChecked.AddListener(() => gameManager.checkBoxVerifier.CheckIfAnsweredAll());
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        isChecked = !isChecked;
        onChecked?.Invoke();
        boxImage.sprite = isChecked ? checkedBox : emptyBox;
    }
}

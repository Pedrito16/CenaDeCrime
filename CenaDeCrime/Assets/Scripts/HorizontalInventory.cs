using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[System.Serializable]
public class HorizontalInventoryItem
{
    public GameObject itemSelectionImage;
    public GameObject itemObject;
}
public class HorizontalInventory : MonoBehaviour
{
    [SerializeField] HorizontalInventoryItem[] items;
    [SerializeField] bool hasItemEquipped = false;
    [SerializeField] bool isUsingItem = false;
    [SerializeField] int currentEquippedItemIndex = 0;
    void Start()
    {
        
    }
    public void OnEquipItemIndex(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            int index = (int)context.ReadValue<float>();
            index -= 1; // Adjust for 0-based index
            print(index);
            if (index >= 0 && index < items.Length)
            {
                if (hasItemEquipped)
                {
                    items[currentEquippedItemIndex].itemSelectionImage.SetActive(false);
                    items[currentEquippedItemIndex].itemObject.SetActive(false);

                    hasItemEquipped = false;
                    return;
                }
                items[index].itemSelectionImage.SetActive(true);
                items[index].itemObject.SetActive(true);

                currentEquippedItemIndex = index;
                hasItemEquipped = true;
            }
        }
    }
    public void OnUseItem(InputAction.CallbackContext context) 
    { 
        if (context.started && hasItemEquipped)
        {
            Items item = items[currentEquippedItemIndex].itemObject.GetComponent<Items>();
            if(!isUsingItem)
            {
                item.UseItem();
                isUsingItem = true;
            }
            else
            {
                item.StopUsingItem();
                isUsingItem = false;
            }
        }
    }
}

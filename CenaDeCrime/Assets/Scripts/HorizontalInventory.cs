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
        GameManager.instance.horizontalInventory = this;
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

                    if(hasItemEquipped && isUsingItem) DeactivateUseItem();
                    hasItemEquipped = false;
                    return;
                }
                items[index].itemSelectionImage.SetActive(true);
                print("use item");
                items[index].itemObject.SetActive(true);

                currentEquippedItemIndex = index;
                hasItemEquipped = true;
            }
        }
    }
    public void DeactivateUseItem()
    {
        if (hasItemEquipped)
        {
            print("stop1");
            items[currentEquippedItemIndex].itemObject.GetComponent<Items>().StopUsingItem();
            print("stop1");
            isUsingItem = false;
        }
    }
    public void OnUseItem(InputAction.CallbackContext context) 
    { 
        if (context.started && hasItemEquipped && !isUsingItem)
        {
            Items item = items[currentEquippedItemIndex].itemObject.GetComponent<Items>();
            item.UseItem();
            isUsingItem = true;
        }
    }
    public void OnStopUseItem(InputAction.CallbackContext context)
    {
        if (context.started && hasItemEquipped && isUsingItem)
        {
            Items item = items[currentEquippedItemIndex].itemObject.GetComponent<Items>();
            item.StopUsingItem();
            isUsingItem = false;
        }
    }
}

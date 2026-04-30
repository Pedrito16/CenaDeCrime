using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class InventoryController : MonoBehaviour
{
    
    [SerializeField] GameObject frontObjectTest;
    [SerializeField] InventoryItem[] inventoryItems;
    [SerializeField] int currentIndex;
    void Start()
    {
        //GameObject instantiatedObject = Instantiate(inventoryObjects[0], transform);
        //instantiatedObject.transform.localPosition = frontObjectTest.transform.localPosition;
        //frontObjectTest.SetActive(false);
    }
    public void Move(int direction)
    {
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].MoveToNext(DistanceFromIndex(i));
        }
    }
    int DistanceFromIndex(int index)
    {
        return Mathf.Abs(currentIndex - index);
    }
}

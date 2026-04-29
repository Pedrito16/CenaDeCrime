using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject frontObjectTest;
    [SerializeField] GameObject[] inventoryObjects;
    [SerializeField] int currentIndex;
    void Start()
    {
        GameObject instantiatedObject = Instantiate(inventoryObjects[0], transform);
        instantiatedObject.transform.localPosition = frontObjectTest.transform.localPosition;
        frontObjectTest.SetActive(false);
    }
    public void Move(int direction)
    {

    }
    int DistanceFromIndex(int index)
    {
        return Mathf.Abs(currentIndex - index);
    }
}

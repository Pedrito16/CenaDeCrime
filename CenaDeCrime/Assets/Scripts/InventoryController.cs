using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject frontObjectTest;
    [SerializeField] GameObject[] inventoryObjects;
    void Start()
    {
        GameObject instantiatedObject = Instantiate(inventoryObjects[0], transform);
        instantiatedObject.transform.localPosition = frontObjectTest.transform.localPosition;
        frontObjectTest.SetActive(false);
    }
}

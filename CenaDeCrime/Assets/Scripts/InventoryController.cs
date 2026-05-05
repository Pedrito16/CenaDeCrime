using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator inventoryAnimator;
    [SerializeField] GameObject inventoryCanvas;

    [Header("Inventory Settings")]
    [SerializeField] float animationTime;
    [SerializeField] float amountToMove;
    [SerializeField] InventoryItem[] inventoryItems;

    [Header("Debug")]
    [SerializeField] int currentIndex;
    [SerializeField] bool isActivated;
    Coroutine animationRoutine;
    void Start()
    {
        inventoryCanvas.SetActive(false);
    }
    public void Move(int direction)
    {
        if (!isActivated) return;

        currentIndex += direction;
        currentIndex = Mathf.Clamp(currentIndex, 0, inventoryItems.Length - 1);
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].MoveToNext(direction * -1, amountToMove, i == currentIndex);
        }
    }
    [ContextMenu("Move Left")]
    public void MoveLeft()
    {
        Move(-1);
    }
    public void ActivateInventory(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(animationRoutine != null) StopCoroutine(animationRoutine);
            inventoryCanvas.SetActive(true);

            isActivated = !isActivated;   
            inventoryAnimator.SetBool("IsActivated", isActivated);
            print("Activated");
            animationRoutine = StartCoroutine(WaitAnimationEnd());
        }
    } 
    public void MoveInventory(InputAction.CallbackContext context)
    {
        if (!isActivated) return;
        if (context.started)
        {
            int direction = (int)context.ReadValue<float>();
            Move(direction);
        }
    }
    IEnumerator WaitAnimationEnd()
    {
        yield return new WaitForSeconds(animationTime);
        inventoryCanvas.SetActive(isActivated);
    }
    int DistanceFromIndex(int index)
    {
        return Mathf.Abs(currentIndex - index);
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator inventoryAnimator;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] RectTransform itemsRawImg;

    [Header("Inventory Settings")]
    [SerializeField] float animationTime;
    [SerializeField] float amountToMove;
    [SerializeField] InventoryItem[] inventoryItems;

    [Header("Rotate mode")]
    [SerializeField] float rotationSpeed;
    [SerializeField] int rotationDirection;

    [Header("Debug")]
    [SerializeField] int currentIndex;
    [SerializeField] bool isActivated;
    [SerializeField] bool isOnRotateMode;
    [SerializeField] bool isRotating;
    
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
    private void Update()
    {
        if(!isActivated && !isOnRotateMode) return;

        if (isRotating)
        {
            Transform selectedItem = inventoryItems[currentIndex].transform;
            selectedItem.Rotate(Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
        }
    }
    IEnumerator WaitAnimationEnd()
    {
        yield return new WaitForSeconds(animationTime);

        if (!isActivated)
        {
            if (isOnRotateMode) ToggleRotate();
            itemsRawImg.anchoredPosition = Vector2.zero;
        }

        inventoryCanvas.SetActive(isActivated);
    }
    #region Inputs
    public void ActivateInventory(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (animationRoutine != null) StopCoroutine(animationRoutine);
            inventoryCanvas.SetActive(true);

            isActivated = !isActivated;

            PlayerMoviment playerMovement = GameManager.instance.playerMovement;
            playerMovement.canMove = !isActivated;
            playerMovement.canMoveCam = !isActivated;

            inventoryAnimator.SetBool("IsActivated", isActivated);
            animationRoutine = StartCoroutine(WaitAnimationEnd());
        }
    }
    public void MoveInventory(InputAction.CallbackContext context)
    {
        if (!isActivated) return;

        int direction = (int)context.ReadValue<float>();

        if (context.started && !isOnRotateMode)
        {
            int newIndex = currentIndex + direction;

            if (newIndex < 0 || newIndex > inventoryItems.Length - 1) return;

            Move(direction);
        }
    }
    public void RotateItem(InputAction.CallbackContext context)
    {
        if (!isActivated || !isOnRotateMode) return;
        if(context.started)
        {
            print("rotate");
            rotationDirection = (int)context.ReadValue<float>();
            isRotating = true;
        }
        if (context.canceled)
        {
            rotationDirection = 0;
            isRotating = false;
        }
    }
    public void ToggleRotateMode(InputAction.CallbackContext context)
    {
        if (!isActivated) return;
        if (context.started)
        {
            ToggleRotate();
        }
    }
    #endregion
    void ToggleRotate()
    {
        isOnRotateMode = !isOnRotateMode;

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (i == currentIndex) continue;
            inventoryItems[i].MoveDownwards(isOnRotateMode);
        }
    }
}

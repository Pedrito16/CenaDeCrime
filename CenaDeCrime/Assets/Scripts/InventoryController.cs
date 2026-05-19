using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator inventoryAnimator;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] RectTransform itemsRawImg;

    [Header("Inventory Settings")]
    [SerializeField] float spaceBetweenItems = 2;
    [SerializeField] float animationTime;
    [SerializeField] float amountToMove;
    [SerializeField] InventoryItem[] inventoryItems;

    [Header("Rotate mode")]
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector2 rotationDirection;
    [SerializeField] UnityEvent onRotateStart;
    [SerializeField] UnityEvent onRotateEnd;

    [Header("Debug")]
    [SerializeField] int currentIndex;
    [SerializeField] bool isActivated;
    [SerializeField] bool isOnRotateMode;
    [SerializeField] bool isRotating;
    InventoryAmountUI inventoryAmountUI;
    Coroutine animationRoutine;
    void Start()
    {
        GameManager.instance.inventoryController = this;
        inventoryAmountUI = GetComponent<InventoryAmountUI>();
        inventoryCanvas.SetActive(false);
        SortInventory();
    }
    public void SortInventory()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            InventoryItem item = inventoryItems[i];
            if (i == currentIndex)
            {
                item.transform.localPosition = Vector3.zero;
                item.MoveToNext(0, 0, true);
                continue;
            }
            item.transform.localPosition = Vector3.zero;
            item.transform.localPosition = new Vector3(spaceBetweenItems * (i - currentIndex), 0, 0);
        }
    }
    public void Move(int direction)
    {
        if (!isActivated) return;

        int newIndex = currentIndex + direction;
        if (newIndex < 0 || newIndex > inventoryItems.Length - 1) return;

        currentIndex += direction;
        currentIndex = Mathf.Clamp(currentIndex, 0, inventoryItems.Length - 1);

        inventoryAmountUI.SelectBall(currentIndex);
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            inventoryItems[i].MoveToNext(direction * -1, amountToMove, i == currentIndex);
        }
    }
    public void AddItem()
    {
        SortInventory();
    }
    private void Update()
    {
        if (!isActivated && !isOnRotateMode) return;

        if (isRotating)
        {
            Transform selectedItem = inventoryItems[currentIndex].transform;
            selectedItem.Rotate(rotationDirection * rotationSpeed * Time.deltaTime);
        }
    }

    #region Inputs
    public void ActivateInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (animationRoutine != null) StopCoroutine(animationRoutine);
            inventoryCanvas.SetActive(true);

            isActivated = !isActivated;
            Cursor.lockState = isActivated ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isActivated;

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
            Move(direction);
    }
    public void RotateItem(InputAction.CallbackContext context)
    {
        if (!isActivated || !isOnRotateMode) return;
        if (context.performed)
        {
            rotationDirection = context.ReadValue<Vector2>();

            isRotating = true;
        }
        if (context.canceled)
        {
            rotationDirection = Vector2.zero;
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
        isRotating = false;
        if (!isOnRotateMode)
        {
            inventoryItems[currentIndex].ResetRotation();
            onRotateEnd.Invoke();
        }
        else
            onRotateStart.Invoke();

        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (i == currentIndex) continue;
            inventoryItems[i].MoveDownwards(isOnRotateMode);
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
}

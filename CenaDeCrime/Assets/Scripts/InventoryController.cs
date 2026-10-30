using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class InventoryController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform lightTransform;
    [SerializeField] GameObject rotationAxis;
    [SerializeField] Transform inventoryCam;
    [SerializeField] Animator inventoryAnimator;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] RectTransform itemsRawImg;

    [Header("Inventory Settings")]
    [SerializeField] Vector3 offset;
    [SerializeField] float spaceBetweenItems = 2;
    [SerializeField] float animationTime;
    [SerializeField] float amountToMove;
    public List<InventoryItem> inventoryItems;

    [Header("Zoom")]
    [SerializeField] float zoomSmoothStep;
    [SerializeField] float maxZoom;   
    [SerializeField] float minZoom;
    [SerializeField] float scrollSensitivity;

    [Header("Rotate mode")]
    [SerializeField] float rotationCooldown = 1f;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationDirection;
    [SerializeField] UnityEvent onRotateStart;
    [SerializeField] UnityEvent onRotateEnd;
    [HideInInspector] public UnityEvent<int> onRecieveIndex;
    float scrollRotation;

    [Header("Debug")]
    [SerializeField] float currentZoom;
    [SerializeField] float initialZoom;
    [SerializeField] float currentZoom01;
    [SerializeField] float scrollValue;
    [SerializeField] float targetZoom01;

    [SerializeField] int currentIndex;
    [SerializeField] bool canRotate;
    public bool isActivated;
    [SerializeField] bool isOnRotateMode;
    [SerializeField] bool isRotating;
    Vector3 rotationAxisOriginalScale;

    InventoryAmountUI inventoryAmountUI;
    Coroutine animationRoutine;
    void Start()
    {
        GameManager.instance.inventoryController = this;

        inventoryAmountUI = GetComponent<InventoryAmountUI>();
        inventoryCanvas.SetActive(false);
        rotationAxisOriginalScale = rotationAxis.transform.localScale;
        canRotate = true;
        initialZoom = inventoryCam.transform.localPosition.z + offset.z;
        onRotateEnd?.Invoke();
        rotationAxis.SetActive(false);

        inventoryCam.transform.localPosition = new Vector3(offset.x, offset.y, initialZoom);

        if (inventoryItems.Count == 0) return;
        
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryAmountUI.SpawnBall();
        }

        SortInventory();
    }
    public void SortInventory()
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            InventoryItem item = inventoryItems[i];
            
            if (i == currentIndex)
            {
                item.transform.localPosition = offset;
                item.MoveToNext(0, 0, true);
                continue;
            }
            item.transform.localPosition = offset;
            item.transform.localPosition = new Vector3(offset.x + spaceBetweenItems * (i - currentIndex), offset.y, offset.z);
        }
    }
    public void Move(int direction)
    {
        if (!isActivated) return;

        int newIndex = currentIndex + direction;
        if (newIndex < 0 || newIndex > inventoryItems.Count - 1) return;

        currentIndex += direction;
        currentIndex = Mathf.Clamp(currentIndex, 0, inventoryItems.Count - 1);

        onRecieveIndex?.Invoke(currentIndex);
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            inventoryItems[i].MoveToNext(direction * -1, amountToMove, i == currentIndex);
        }
    }
    public void AddItem(InventoryItem newItem)
    {
        inventoryAmountUI.SpawnBall();
        inventoryItems.Add(newItem);
        newItem.transform.SetParent(transform);
        SortInventory();
    }
    private void Update()
    {
        if (!isActivated && !isOnRotateMode) return;

        currentZoom01 = Mathf.Lerp(currentZoom01, targetZoom01, Time.deltaTime * zoomSmoothStep);
        float z = Mathf.Lerp(maxZoom, minZoom, currentZoom01); //suavizacao de zoom
        inventoryCam.transform.localPosition = new Vector3(offset.x, offset.y, z);


        if (isRotating)
        {
            if(inventoryItems.Count <= 0) return;

            Transform selectedItem = inventoryItems[currentIndex].transform;
            selectedItem.Rotate(rotationDirection.normalized * rotationSpeed * Time.deltaTime);
        }
    }
    private void LateUpdate()
    {
        if(!isOnRotateMode && isRotating) return;
        if(!isActivated || inventoryItems.Count <= 0) return;
        if(rotationAxis.activeSelf == false) return;
        rotationAxis.transform.position = inventoryItems[currentIndex].transform.position;
        rotationAxis.transform.rotation = inventoryItems[currentIndex].transform.rotation;

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
            rotationDirection = context.ReadValue<Vector3>();
            rotationAxis.SetActive(true);

            isRotating = true;
        }
        if (context.canceled)
        {
            rotationAxis.SetActive(false);
            rotationDirection = Vector3.zero;
            isRotating = false;
        }
    }
    public void ToggleRotateMode(InputAction.CallbackContext context)
    {
        if (!isActivated || inventoryItems.Count <= 0) return;
        if (context.started && canRotate)
            ToggleRotate();
    }
    public void ScrollMouse(InputAction.CallbackContext context)
    {
        if (!isActivated) return;

        scrollValue = context.ReadValue<float>();
        scrollValue = Mathf.Clamp(scrollValue, -1, 1);

        targetZoom01 += scrollValue * scrollSensitivity;
        targetZoom01 = Mathf.Clamp01(targetZoom01);
    }
    #endregion
    void ToggleRotate()
    {
        isOnRotateMode = !isOnRotateMode;
        print("toggle rotate: " + isOnRotateMode);
        isRotating = false;
        if (!isOnRotateMode)
        {
            rotationAxis.SetActive(false);
            inventoryCam.localPosition = new Vector3(offset.x, offset.y, initialZoom);
            inventoryItems[currentIndex].ResetRotation();
            onRotateEnd.Invoke();
            StartCoroutine(RotateCooldown());
        }
        else
            onRotateStart.Invoke();

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (i == currentIndex)
            {
                rotationAxis.transform.localScale = rotationAxisOriginalScale;
                continue;
            }
            inventoryItems[i].MoveDownwards(isOnRotateMode);
        }
    }
    IEnumerator RotateCooldown()
    {
        canRotate = false;
        yield return new WaitForSeconds(rotationCooldown);
        canRotate = true;
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

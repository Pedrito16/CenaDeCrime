using UnityEngine;

public class MenuCameraFollow : MonoBehaviour
{
    [Header("Referências")]
    public Transform cameraTransform;

    [Header("Movimento")]
    public float maxRotationX = 5f;
    public float maxRotationY = 8f;

    [Header("Suavização")]
    public float smoothSpeed = 5f;

    private Quaternion initialRotation;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        initialRotation = cameraTransform.localRotation;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Posição do mouse normalizada (-1 até 1)
        float mouseX = (Input.mousePosition.x / Screen.width) * 2f - 1f;
        float mouseY = (Input.mousePosition.y / Screen.height) * 2f - 1f;

        // Rotação alvo
        float rotY = mouseX * maxRotationY;
        float rotX = -mouseY * maxRotationX;

        Quaternion targetRotation =
            initialRotation * Quaternion.Euler(rotX, rotY, 0);

        // Suavização
        cameraTransform.localRotation = Quaternion.Lerp(
            cameraTransform.localRotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}
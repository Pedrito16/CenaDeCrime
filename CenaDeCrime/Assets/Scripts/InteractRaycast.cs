using UnityEngine;
using UnityEngine.InputSystem;
public class InteractRaycast : MonoBehaviour
{
    [SerializeField] float interactDistance = 3f;
    [SerializeField] LayerMask ignoreLayer;

    [Header("Debug")]
    [SerializeField] bool enteredRaycast;
    [SerializeField] Interactable currentInteractable;

    Interactable lastInteractable;
    void Start()
    {
        
    }
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, ignoreLayer))
        {
            if(hit.collider.TryGetComponent(out Interactable interactable))
            {
                currentInteractable = interactable;
                if (interactable != null && !enteredRaycast)
                {
                    print("Entered raycast");
                    interactable.OnRaycastEnter();
                    enteredRaycast = true;
                }
                lastInteractable = interactable;
            }
        }
        else
        {
            if (!enteredRaycast) return;

            if (lastInteractable != null)
            {
                print("Exited raycast");
                lastInteractable.OnRaycastExit();
                lastInteractable = null;
                enteredRaycast = false;
            }
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (lastInteractable != null)
            {
                lastInteractable.Interact();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawRay(transform.position, transform.forward * interactDistance);
    }
}

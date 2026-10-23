using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public class InteractRaycast : MonoBehaviour
{
    [SerializeField] FocusOnLook focusSystem;
    [SerializeField] float interactDistance = 3f;
    [SerializeField] LayerMask ignoreLayer;

    [Header("Debug")]
    [SerializeField] bool enteredRaycast;
    public UnityEvent onEvidenceEnterExit;
    Interactable lastInteractable;
    void Awake()
    {
        GameManager.instance.interactRaycast = this;
    }
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, ignoreLayer))
        {
            if(hit.collider.TryGetComponent(out Interactable interactable))
            {
                if (interactable != null && !enteredRaycast)
                {

                    if(interactable is EvidenceCollector)
                        onEvidenceEnterExit?.Invoke();

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
                if(lastInteractable is EvidenceCollector)
                    onEvidenceEnterExit?.Invoke();

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

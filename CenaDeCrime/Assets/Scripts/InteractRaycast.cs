using UnityEngine;

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
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.aliceBlue;
        Gizmos.DrawRay(transform.position, transform.forward * interactDistance);
    }
}

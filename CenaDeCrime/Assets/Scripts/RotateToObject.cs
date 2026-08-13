using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class RotateToObject : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 rotationOffsets;
    [SerializeField] Vector2 originalPos;
    void Start()
    {
        originalPos = transform.position;
    }
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        Debug.DrawRay(transform.position, direction, Color.red);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.forward = Quaternion.Euler(90, direction.y, 0);
    }
}

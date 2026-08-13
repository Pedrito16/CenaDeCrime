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
        Vector3 targetPos = new Vector3(transform.position.x + rotationOffsets.x, transform.position.y + rotationOffsets.y, target.position.z + rotationOffsets.z);
        transform.LookAt(targetPos);

    }
}

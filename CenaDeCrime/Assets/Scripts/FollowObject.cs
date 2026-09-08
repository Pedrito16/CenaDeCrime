using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Transform target;
    [SerializeField] Transform rotationTarget;

    [Header("Configs")]
    [SerializeField] Vector3 rotationOffset;
    [SerializeField] float speed = 1;
    [SerializeField] AnimationCurve curve;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, curve.Evaluate(speed * Time.deltaTime));
        Quaternion targetRotation = Quaternion.LookRotation(rotationTarget.position - transform.position);
        transform.rotation = targetRotation * Quaternion.Euler(rotationOffset);
    }
}

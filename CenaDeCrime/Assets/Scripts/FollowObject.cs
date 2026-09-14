using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Transform target;
    [SerializeField] Transform frontFaceTarget;
    [SerializeField] Transform rotationTarget;

    [Header("Configs")]
    [SerializeField] bool useFrontFaceTarget = false;
    [SerializeField] Vector3 rotationOffset;
    [SerializeField] Vector3 frontFaceRotationOffset;
    [Space(5)]
    [SerializeField] float speed = 1;
    [SerializeField] AnimationCurve curve;
    void Start()
    {
        
    }
    void Update()
    {
        Transform currentTarget = useFrontFaceTarget ? frontFaceTarget : target;
        transform.position = Vector3.Lerp(transform.position, currentTarget.position, curve.Evaluate(speed * Time.deltaTime));
        Quaternion targetRotation = Quaternion.LookRotation(rotationTarget.position - transform.position);
        if (!useFrontFaceTarget)
            transform.rotation = targetRotation * Quaternion.Euler(rotationOffset);
        else
            transform.rotation = targetRotation * Quaternion.Euler(frontFaceRotationOffset);
    }
    public void SwitchTarget(bool useFrontFace)
    {
        useFrontFaceTarget = useFrontFace;
    }
}

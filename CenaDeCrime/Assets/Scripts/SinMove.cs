using UnityEngine;
using UnityEngine.UI;
public enum Axis
{
    X,
    Y,
    Z
}
public class SinMove : MonoBehaviour
{
    [SerializeField] float amplitude;
    [SerializeField] float frequency;
    [SerializeField] Axis axis;

    RectTransform rectTransform;

    Vector3 axisVector;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        switch (axis)
        {
            case Axis.X: axisVector = Vector3.right; break;
            case Axis.Y: axisVector = Vector3.up; break;
            case Axis.Z: axisVector = Vector3.forward; break;
        }
    }
    void Update()
    {
        float sin = Mathf.Sin(Time.time * frequency) * amplitude;
        if(rectTransform != null)
            rectTransform.anchoredPosition = axisVector * sin;
        else
            transform.position = transform.root.position + axisVector * sin;
        
    }
}   
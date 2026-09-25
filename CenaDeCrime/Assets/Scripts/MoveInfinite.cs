using UnityEngine;

public class MoveInfinite : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] float timeToReset = 10f;
    [SerializeField] Vector3 vectorToMove;

    [Header("Debug")]
    [SerializeField] Vector3 startPos;
    [SerializeField] float timer;
    void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToReset)
        {
            timer = 0f;
            transform.position = startPos;
        }
        transform.position += vectorToMove * speed * Time.deltaTime;
    }
}

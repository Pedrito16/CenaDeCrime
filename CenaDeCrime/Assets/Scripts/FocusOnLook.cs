using System.Collections;
using UnityEngine;

public class FocusOnLook : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera cam;

    [Header("Detection")]
    [SerializeField] float lookTime = 3f;

    [Header("Zoom")]
    [SerializeField] float targetFOV = 25;
    [SerializeField] float normalFOV = 60;
    [SerializeField] float duration = .45f;

    float timer;
    Coroutine focusRoutine;
    Transform currentTarget;

    public void LookingAt(Transform target)
    {
        if (currentTarget != target)
        {
            currentTarget = target;
            timer = 0;
        }

        timer += Time.deltaTime;

        if (timer >= lookTime && focusRoutine == null)
        {
            focusRoutine = StartCoroutine(Focus(target));
        }
    }

    public void StopLooking()
    {
        currentTarget = null;
        timer = 0;
    }

    IEnumerator Focus(Transform target)
    {
        Quaternion startRot = cam.transform.rotation;
        Quaternion endRot =
            Quaternion.LookRotation(target.position - cam.transform.position);

        float startFov = cam.fieldOfView;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;

            // Ease Out
            float curve = 1 - Mathf.Pow(1 - t, 3);

            cam.transform.rotation =
                Quaternion.Slerp(startRot, endRot, curve);

            cam.fieldOfView =
                Mathf.Lerp(startFov, targetFOV, curve);

            yield return null;
        }

        yield return new WaitForSeconds(1.2f);

        t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / duration;

            float curve = 1 - Mathf.Pow(1 - t, 3);

            cam.fieldOfView =
                Mathf.Lerp(targetFOV, normalFOV, curve);

            yield return null;
        }

        focusRoutine = null;
        timer = 0;
    }
}
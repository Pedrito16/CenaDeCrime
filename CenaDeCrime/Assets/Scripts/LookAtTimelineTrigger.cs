using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class LookAtTimelineTrigger : MonoBehaviour
{
    [Header("Raycast")]
    public float maxDistance = 20f;
    public LayerMask targetLayers;
    public Transform target;
    public GameObject[] objectsToActivate;

    [Header("Tempo para ativar")]
    public float lookDuration = 2f;
    public float cooldown = 2f;

    [Header("Timeline")]
    public PlayableDirector timeline;

    [Header("Movimento da câmera")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float timeToActivateCamControl = 0.5f;
    [SerializeField] bool alreadyPlayedCutscene = false;

    [Header("Zoom")]
    public float zoomFOV = 30f;
    public float zoomSpeed = 2f;

    private float lookTimer = 0f;
    private float cooldownTimer = 0f;

    private bool cutsceneRunning = false;

    private PlayerMoviment playerMovement;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (timeline == null)
            Debug.LogError("LookAtTimelineTrigger: Timeline não configurada.");

        if (GameManager.instance != null)
            playerMovement = GameManager.instance.playerMovement;

        if (timeline != null)
            timeline.stopped += OnTimelineStopped;
    }

    private void OnDestroy()
    {
        if (timeline != null)
            timeline.stopped -= OnTimelineStopped;
    }

    private void Update()
    {
        if(alreadyPlayedCutscene)
            return;

        if (cutsceneRunning)
            return;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0f)
            return;

        CheckLookingAtTarget();
    }
    private void CheckLookingAtTarget()
    {
        if (playerCamera == null || target == null)
            return;

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            maxDistance,
            targetLayers,
            QueryTriggerInteraction.Ignore))
        {
            if (hit.transform == target || hit.transform.IsChildOf(target))
            {
                lookTimer += Time.deltaTime;

                if (lookTimer >= lookDuration)
                    StartCutscene();

                return;
            }
        }

        // Parou de olhar para o objeto
        lookTimer = 0f;
    }

    private void StartCutscene()
    {
        if (timeline == null)
            return;

        if (cutsceneRunning)
            return;

        ToggleActivatedObjects();

        cutsceneRunning = true;
        cooldownTimer = Mathf.Max(2f, cooldown);
        playerCamera.enabled = true;

        if (playerMovement != null)
        {
            playerMovement.canMove = false;
            playerMovement.canMoveCam = false;
        }

        timeline.Play();
    }
    void ToggleActivatedObjects()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            if (objectsToActivate[i] != null)
                objectsToActivate[i].SetActive(!objectsToActivate[i].activeSelf);
        }
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director != timeline)
            return;

        cutsceneRunning = false;
        playerCamera.enabled = true;
        alreadyPlayedCutscene = true;

        ToggleActivatedObjects();
        StartCoroutine(ToggleCamCooldown());
    }
    IEnumerator ToggleCamCooldown()
    {
        yield return new WaitForSeconds(timeToActivateCamControl);
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
            playerMovement.canMoveCam = true;
        }
    }
}
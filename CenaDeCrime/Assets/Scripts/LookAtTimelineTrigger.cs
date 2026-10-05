using UnityEngine;
using UnityEngine.Playables;

public class LookAtTimelineTrigger : MonoBehaviour
{
    [Header("Raycast")]
    public Camera playerCamera;
    public float maxDistance = 20f;
    public LayerMask targetLayers;
    public Transform target;

    [Header("Tempo para ativar")]
    public float lookDuration = 2f;
    public float cooldown = 2f;

    [Header("Timeline")]
    public PlayableDirector timeline;

    [Header("Camera durante a cutscene")]
    public Transform cameraFollowTarget;

    [Tooltip("Objeto/Transform para onde a câmera vai olhar. Pode ser o mesmo objeto.")]
    public Transform cameraLookTarget;

    [Header("Movimento da câmera")]
    public float cameraFollowSmooth = 5f;
    public float cameraRotationSmooth = 5f;

    [Header("Zoom")]
    public float zoomFOV = 30f;
    public float zoomSpeed = 2f;

    private float lookTimer = 0f;
    private float cooldownTimer = 0f;

    private bool cutsceneRunning = false;

    private float normalFOV;

    private Vector3 cameraOffset;

    private PlayerMoviment playerMovement;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (timeline == null)
            Debug.LogError("LookAtTimelineTrigger: Timeline não configurada.");

        if (playerCamera != null)
            normalFOV = playerCamera.fieldOfView;

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
        if (cutsceneRunning)
            return;

        if (cooldownTimer > 0f)
            cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0f)
            return;

        CheckLookingAtTarget();
    }

    private void LateUpdate()
    {
        if (!cutsceneRunning)
            return;

        FollowObjectWithCamera();
        ZoomCamera();
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
                {
                    StartCutscene();
                }

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

        cutsceneRunning = true;

        cooldownTimer = Mathf.Max(2f, cooldown);

        // Salva o FOV original
        normalFOV = playerCamera.fieldOfView;

        // Se não foi definido um objeto separado para olhar,
        // usa o próprio objeto da câmera.
        if (cameraLookTarget == null)
            cameraLookTarget = cameraFollowTarget;

        // Guarda a posição relativa atual da câmera em relação
        // ao objeto que ela deverá seguir.
        if (cameraFollowTarget != null)
        {
            cameraOffset =
                playerCamera.transform.position -
                cameraFollowTarget.position;
        }

        // Bloqueia o controle do jogador
        if (playerMovement != null)
        {
            playerMovement.canMove = false;
            playerMovement.canMoveCam = false;
        }

        timeline.Play();
    }

    private void FollowObjectWithCamera()
    {
        if (cameraFollowTarget == null)
            return;

        /*
         * A câmera mantém a mesma distância relativa do objeto.
         * Assim, se o objeto andar, a câmera acompanha.
         */
        Vector3 desiredPosition =
            cameraFollowTarget.position + cameraOffset;

        playerCamera.transform.position = Vector3.Lerp(
            playerCamera.transform.position,
            desiredPosition,
            cameraFollowSmooth * Time.deltaTime
        );

        if (cameraLookTarget != null)
        {
            Vector3 direction =
                cameraLookTarget.position -
                playerCamera.transform.position;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion desiredRotation =
                    Quaternion.LookRotation(direction);

                playerCamera.transform.rotation =
                    Quaternion.Slerp(
                        playerCamera.transform.rotation,
                        desiredRotation,
                        cameraRotationSmooth * Time.deltaTime
                    );
            }
        }
    }

    private void ZoomCamera()
    {
        if (playerCamera == null)
            return;

        playerCamera.fieldOfView = Mathf.Lerp(
            playerCamera.fieldOfView,
            zoomFOV,
            zoomSpeed * Time.deltaTime
        );
    }

    private void OnTimelineStopped(PlayableDirector director)
    {
        if (director != timeline)
            return;

        cutsceneRunning = false;

        // Devolve o controle ao jogador
        if (playerMovement != null)
        {
            playerMovement.canMove = true;
            playerMovement.canMoveCam = true;
        }

        // Volta o zoom suavemente
        StartCoroutine(RestoreCameraFOV());
    }

    private System.Collections.IEnumerator RestoreCameraFOV()
    {
        float startFOV = playerCamera.fieldOfView;
        float elapsed = 0f;

        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;

            playerCamera.fieldOfView =
                Mathf.Lerp(startFOV, normalFOV, t);

            yield return null;
        }

        playerCamera.fieldOfView = normalFOV;
    }
}
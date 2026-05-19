using UnityEngine;
using UnityEngine.Playables;

public class MonitorInteractive : MonoBehaviour
{
    [Header("Tela do monitor")]
    public MeshRenderer monitorRenderer;

    [Header("Materiais")]
    public Material monitorLigado;
    public Material monitorDesligado;

    [Header("Luzes desse monitor")]
    public Light[] monitorLights;

    [Header("Timeline")]
    public PlayableDirector timeline;

    [Header("Script da câmera do player")]
    public MonoBehaviour cameraController;

    [SerializeField] bool mouseEmCima = false;

    private bool estaNaTimeline = false;

    void Start()
    {
        DesligarMonitor();
    }

    void Update()
    {
        // Clique esquerdo
        if (mouseEmCima && Input.GetMouseButtonDown(0))
        {
            if (timeline != null)
            {
                // Desativa câmera do player
                if (cameraController != null)
                {
                    cameraController.enabled = false;
                }

                // Toca Timeline
                timeline.Play();

                estaNaTimeline = true;
            }
        }

        // ESC para voltar
        if (estaNaTimeline && Input.GetKeyDown(KeyCode.Escape))
        {
            SairDaTimeline();
        }
    }

    void SairDaTimeline()
    {
        if (timeline != null)
        {
            // Volta Timeline pro início
            timeline.time = 0;

            // Atualiza imediatamente
            timeline.Evaluate();

            // Para Timeline
            timeline.Stop();
        }

        // Reativa câmera do player
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        estaNaTimeline = false;
    }

    void OnMouseEnter()
    {
        mouseEmCima = true;
        LigarMonitor();
    }

    void OnMouseExit()
    {
        mouseEmCima = false;
        DesligarMonitor();
    }

    void LigarMonitor()
    {
        if (monitorRenderer != null)
        {
            monitorRenderer.material = monitorLigado;
        }

        foreach (Light luz in monitorLights)
        {
            if (luz != null)
                luz.enabled = true;
        }
    }

    void DesligarMonitor()
    {
        if (monitorRenderer != null)
        {
            monitorRenderer.material = monitorDesligado;
        }

        foreach (Light luz in monitorLights)
        {
            if (luz != null)
                luz.enabled = false;
        }
    }
}
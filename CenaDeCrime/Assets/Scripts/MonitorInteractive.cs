using UnityEngine;

public class MonitorInteractive : MonoBehaviour
{
    [Header("Tela do monitor")]
    public MeshRenderer monitorRenderer;

    [Header("Materiais")]
    public Material monitorLigado;
    public Material monitorDesligado;

    [Header("Luzes desse monitor")]
    public Light[] monitorLights;

    void Start()
    {
        DesligarMonitor();
    }

    void OnMouseEnter()
    {
        LigarMonitor();
    }

    void OnMouseExit()
    {
        DesligarMonitor();
    }

    void LigarMonitor()
    {
        // Troca textura/material
        if (monitorRenderer != null)
        {
            monitorRenderer.material = monitorLigado;
        }

        // Liga todas as luzes desse monitor
        foreach (Light luz in monitorLights)
        {
            if (luz != null)
                luz.enabled = true;
        }
    }

    void DesligarMonitor()
    {
        // Troca textura/material
        if (monitorRenderer != null)
        {
            monitorRenderer.material = monitorDesligado;
        }

        // Desliga todas as luzes desse monitor
        foreach (Light luz in monitorLights)
        {
            if (luz != null)
                luz.enabled = false;
        }
    }
}
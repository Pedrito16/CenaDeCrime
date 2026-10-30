using UnityEngine;

public class CreditosSubindo : MonoBehaviour
{
    public float velocidade = 50f;
    public float posicaoInicialY = -600f;
    public float posicaoFinalY = 1500f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        Vector2 posicao = rectTransform.anchoredPosition;
        posicao.y = posicaoInicialY;
        rectTransform.anchoredPosition = posicao;
    }

    void Update()
    {
        Vector2 posicao = rectTransform.anchoredPosition;

        posicao.y += velocidade * Time.deltaTime;

        rectTransform.anchoredPosition = posicao;

        if (posicao.y >= posicaoFinalY)
        {
            enabled = false;
        }
    }
}
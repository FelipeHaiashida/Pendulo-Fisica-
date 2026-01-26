using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PendulumPhysics : MonoBehaviour
{
    [Header("Configurações do Pêndulo")]
    [Range(-15f, 90f)]
    public float anguloInicial = 45f;

    [Header("Física")]
    public float comprimentoBraco = 2f;
    public float taxaAmortecimento = 0.1f;

    [Header("UI - Controles")]
    public Button botaoIniciar;
    public Slider sliderAngulo;

    [Header("UI - Textos")]
    public TMP_Text textoAngulo;
    public TMP_Text textoVelocidade;
    public TMP_Text textoPeriodo;

    private float velocidadeAngular = 0f;
    private float anguloAtual = 0f;
    private bool emMovimento = false;
    private float gravidade;

    void Start()
    {
        SetGravidadeMediaGlobal(); // Já chama o ResetarPendulo e atualiza textos

        if (botaoIniciar != null) botaoIniciar.onClick.AddListener(IniciarPendulo);

        if (sliderAngulo != null)
        {
            sliderAngulo.minValue = -15f;
            sliderAngulo.maxValue = 90f;
            sliderAngulo.value = anguloInicial;
            sliderAngulo.onValueChanged.AddListener(AtualizarAngulo);
        }
    }

    void Update()
    {
        // ATUALIZAÇÃO CONTÍNUA DO PERÍODO
        // Isso garante que se você mudar o Comprimento do Braço ou o Ângulo no Inspector
        // durante o jogo, o texto do período se atualiza automaticamente.
        AtualizarTextoPeriodo();

        if (emMovimento)
        {
            float aceleracaoAngular = -(gravidade / comprimentoBraco) * Mathf.Sin(anguloAtual * Mathf.Deg2Rad);
            velocidadeAngular += aceleracaoAngular * Time.deltaTime;
            velocidadeAngular *= Mathf.Exp(-taxaAmortecimento * Time.deltaTime);
            anguloAtual += velocidadeAngular * Mathf.Rad2Deg * Time.deltaTime;

            AtualizarRotacao();

            if (Mathf.Abs(velocidadeAngular) < 0.001f && Mathf.Abs(anguloAtual * Mathf.Deg2Rad) < 0.001f)
            {
                emMovimento = false;
                anguloAtual = 0f;
                velocidadeAngular = 0f;
                AtualizarRotacao();
                AtualizarTextoAngulo();
                if (sliderAngulo != null) sliderAngulo.value = 0f;
            }
        }

        if (textoVelocidade != null)
        {
            float velocidadeLinear = velocidadeAngular * comprimentoBraco;
            textoVelocidade.text = $"Vel. Linear: {Mathf.Abs(velocidadeLinear):F2} m/s";
        }
    }

    void IniciarPendulo()
    {
        if (!emMovimento)
        {
            emMovimento = true;
            velocidadeAngular = 0f;
            anguloAtual = anguloInicial;
        }
    }

    void AtualizarAngulo(float novoAngulo)
    {
        if (!emMovimento)
        {
            anguloInicial = novoAngulo;
            anguloAtual = novoAngulo;
            AtualizarRotacao();
            AtualizarTextoAngulo();
            // O período muda quando mudamos o ângulo inicial!
            AtualizarTextoPeriodo();
        }
    }

    void AtualizarRotacao()
    {
        transform.localRotation = Quaternion.Euler(0, 0, anguloAtual);
    }

    void AtualizarTextoAngulo()
    {
        if (textoAngulo != null) textoAngulo.text = $"Ângulo: {anguloAtual:F1}°";
    }

    // --- CORREÇÃO AQUI ---
    void AtualizarTextoPeriodo()
    {
        if (textoPeriodo != null)
        {
            // 1. Período Base (Pequenas oscilações)
            float T0 = 2 * Mathf.PI * Mathf.Sqrt(comprimentoBraco / gravidade);

            // 2. Fator de Correção para Grandes Ângulos (Série de Bernoulli)
            // Fórmula: T = T0 * (1 + theta^2 / 16)
            // O ângulo deve estar em Radianos e ser positivo (Abs)
            float theta0 = Mathf.Abs(anguloInicial) * Mathf.Deg2Rad;
            float correcao = 1.0f + (Mathf.Pow(theta0, 2) / 16.0f);

            float periodoReal = T0 * correcao;

            textoPeriodo.text = $"Período (T): {periodoReal:F2} s";
        }
    }

    public void ResetarPendulo()
    {
        emMovimento = false;
        velocidadeAngular = 0f;
        anguloAtual = anguloInicial;
        AtualizarRotacao();
        AtualizarTextoAngulo();
        AtualizarTextoPeriodo();
        if (sliderAngulo != null) sliderAngulo.value = anguloInicial;
    }

    // --- GRAVIDADE ---
    public void SetGravidadeMediaGlobal() { gravidade = 9.81f; ResetarPendulo(); }
    public void SetGravidadeBrasil() { gravidade = 9.788f; ResetarPendulo(); }
    public void SetGravidadeEUA() { gravidade = 9.800f; ResetarPendulo(); }
    public void SetGravidadeAustralia() { gravidade = 9.797f; ResetarPendulo(); }
    public void SetGravidadeIndia() { gravidade = 9.791f; ResetarPendulo(); }
    public void SetGravidadeRussia() { gravidade = 9.815f; ResetarPendulo(); }

    // --- ANGULOS ---
    public void SetAnguloInicial(float novoAngulo)
    {
        novoAngulo = Mathf.Clamp(novoAngulo, -15f, 90f);
        if (!emMovimento)
        {
            anguloInicial = novoAngulo;
            anguloAtual = novoAngulo;
            if (sliderAngulo != null) sliderAngulo.value = novoAngulo;
            AtualizarRotacao();
            AtualizarTextoAngulo();
            AtualizarTextoPeriodo(); // Atualiza o período ao clicar nos botões
        }
        else { anguloInicial = novoAngulo; }
    }

    public void SetAnguloMenos15() { SetAnguloInicial(-15f); }
    public void SetAngulo30() { SetAnguloInicial(30f); }
    public void SetAngulo45() { SetAnguloInicial(45f); }
    public void SetAngulo60() { SetAnguloInicial(60f); }
    public void SetAngulo90() { SetAnguloInicial(90f); }
}
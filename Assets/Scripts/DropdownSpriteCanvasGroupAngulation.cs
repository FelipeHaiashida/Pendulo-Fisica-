using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class AnguloOpcao
{
    public string codigo;                   // "30", "45", "60" etc.
    public Sprite spriteCompleta;           // Sprite do botão
    public Sprite spritePendulo;            // Imagem do pêndulo para este ângulo
    public Button botao;                    // Botão clicável
}

public class DropdownSpriteCanvasGroupAngulation : MonoBehaviour
{
    [Header("Referências Visuais")]
    public Image imagemSelecionada;         // Sprite no botão principal
    public Image imagemPendulo;             // Sprite do pêndulo correspondente
    public CanvasGroup grupoLista;          // Lista de opções

    [Header("Input do Ângulo")]
    public TMP_InputField inputAngulo;      // <- ADICIONADO AQUI

    [Header("Opções")]
    public List<AnguloOpcao> opcoes;

    private bool listaAberta = false;
    private string anguloSelecionado;

    void Start()
    {
        FecharLista();

        foreach (var opcao in opcoes)
        {
            string codigo = opcao.codigo;
            opcao.botao.image.sprite = opcao.spriteCompleta;

            // Captura o valor local corretamente
            opcao.botao.onClick.AddListener(() => SelecionarAngulo(codigo));
        }
    }

    public void AlternarLista()
    {
        if (listaAberta)
            FecharLista();
        else
            AbrirLista();
    }

    private void AbrirLista()
    {
        grupoLista.alpha = 1f;
        grupoLista.interactable = true;
        grupoLista.blocksRaycasts = true;
        listaAberta = true;
    }

    private void FecharLista()
    {
        grupoLista.alpha = 0f;
        grupoLista.interactable = false;
        // ATENÇÃO: blocksRaycasts fica como está (pode ser true)
        listaAberta = false;
    }

    private void SelecionarAngulo(string codigo)
    {
        anguloSelecionado = codigo;

        foreach (var opcao in opcoes)
        {
            if (opcao.codigo == codigo)
            {      
                imagemSelecionada.sprite = opcao.spriteCompleta;
               
                if (imagemPendulo != null && opcao.spritePendulo != null)
                    imagemPendulo.sprite = opcao.spritePendulo;

                if (inputAngulo != null)
                    inputAngulo.text = codigo;

                break;
            }
        }

        FecharLista();
    }
}

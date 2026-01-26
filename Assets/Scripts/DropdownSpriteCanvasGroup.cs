using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PaisOpcao
{
    public string codigo;
    public Sprite spriteCompleta;
    public Button botao;
}

public class DropdownSpriteCanvasGroup : MonoBehaviour
{
    [Header("Referências")]
    public Image imagemSelecionada;                 // Imagem do botão principal
    public CanvasGroup grupoLista;                  // CanvasGroup do painel de opções
    public List<PaisOpcao> opcoes;

    private bool listaAberta = false;
    private string paisSelecionado;

    void Start()
    {
        // Inicialmente esconde a lista
        FecharLista();

        foreach (var opcao in opcoes)
        {
            string codigo = opcao.codigo;
            opcao.botao.image.sprite = opcao.spriteCompleta;

            opcao.botao.onClick.AddListener(() => SelecionarPais(codigo));
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
        listaAberta = true;
    }

    private void FecharLista()
    {
        grupoLista.alpha = 0f;
        grupoLista.interactable = false;
        listaAberta = false;
    }

    private void SelecionarPais(string codigo)
    {
        paisSelecionado = codigo;

        foreach (var opcao in opcoes)
        {
            if (opcao.codigo == codigo)
            {
                imagemSelecionada.sprite = opcao.spriteCompleta;
                break;
            }
        }

        FecharLista();
    }
}

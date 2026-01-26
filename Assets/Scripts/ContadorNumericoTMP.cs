using UnityEngine;
using TMPro;

public class ContadorNumericoTMP : MonoBehaviour
{
    [Header("Exibicao do valor")]
    public TMP_InputField campoValor;
    public InputAnguloController controladorAngulo;

    [Header("Configuracao")]
    public int minimo = -90;
    public int maximo = 90;

    public void AlterarValor(int incremento)
    {
        int valorAtual = 0;

        int.TryParse(campoValor.text, out valorAtual);

        valorAtual += incremento;
        valorAtual = Mathf.Clamp(valorAtual, minimo, maximo);

        campoValor.text = valorAtual.ToString();

        // 🔥 Atualiza o pêndulo automaticamente
        if (controladorAngulo != null)
            controladorAngulo.AtualizarAnguloPeloInput();
    }
}

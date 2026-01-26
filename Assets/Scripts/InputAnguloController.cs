using UnityEngine;
using TMPro;

public class InputAnguloController : MonoBehaviour
{
    public TMP_InputField inputAngulo;
    public PendulumPhysics pendulo;

    public void AtualizarAnguloPeloInput()
    {
        if (pendulo == null || inputAngulo == null)
            return;

        int angulo = 0;
        if (int.TryParse(inputAngulo.text, out angulo))
        {
            pendulo.SetAnguloInicial(angulo);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class PaisData
{
    public string nome;
    public Sprite icon;
}

public class CustomDropdown : MonoBehaviour
{
    [Header("Referência ao Dropdown")]
    public Dropdown dropdown;
    public Image selectedImageDisplay; // Mostra a sprite selecionada
    public List<PaisData> paises = new List<PaisData>();

    private void Start()
    {
        ConfigurarDropdown();
    }

    void ConfigurarDropdown()
    {
        dropdown.ClearOptions();

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach (var pais in paises)
        {
            // Apenas Sprite, sem texto
            Dropdown.OptionData option = new Dropdown.OptionData("", pais.icon);
            options.Add(option);
        }

        dropdown.AddOptions(options);

        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        OnDropdownValueChanged(dropdown.value);
    }

    void OnDropdownValueChanged(int index)
    {
        if (index < 0 || index >= paises.Count)
            return;

        selectedImageDisplay.sprite = paises[index].icon;
    }
}

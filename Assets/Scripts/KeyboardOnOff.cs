using UnityEngine;

public class KeyboardnOff : MonoBehaviour
{
    public GameObject keyboard;
    public bool isActive;

    void Start()
    {
        keyboard.SetActive(isActive);
    }

    public void OnOffKeyboard()
    {
        isActive = !isActive;
        keyboard.SetActive(isActive);
    }
}

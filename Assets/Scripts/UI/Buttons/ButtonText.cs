using TMPro;
using UnityEngine;

public class ButtonText : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void Set(string text)
    {
        _text.text = text;
    }
}
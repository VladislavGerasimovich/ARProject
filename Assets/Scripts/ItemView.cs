using TMPro;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void SetView(ItemData itemData)
    {
        _text.text = itemData.Label;
    }
}
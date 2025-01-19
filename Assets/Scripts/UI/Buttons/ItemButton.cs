using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ItemButton : MonoBehaviour
{
    private Button _button;
    private ItemData _itemData;

    public event Action<ItemData> Click;
    public event Action<ItemButton> ButtonDisabled;

    public void Init(ItemData itemData)
    {
        _itemData = itemData;
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonCLick);
    }

    private void OnDisable()
    {
        ButtonDisabled?.Invoke(this);
        _button.onClick.RemoveListener(OnButtonCLick);
    }

    private void OnButtonCLick()
    {
        Click?.Invoke(_itemData);
    }
}
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorButton : MonoBehaviour
{
    private Button _button;
    private int _index;

    public event Action<int> Click;
    public event Action<ColorButton> ButtonDisabled;

    public void Init(int index)
    {
        _index = index;
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
        Click?.Invoke(_index);
    }
}
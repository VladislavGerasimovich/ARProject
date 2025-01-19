using System.Collections.Generic;
using UnityEngine;

public class UIColorPanel : MonoBehaviour
{
    [SerializeField] private Transform _buttonPrefab;
    [SerializeField] private Transform _container;

    private List<ColorButton> _buttons;
    private ModelView _currentModelView;

    private void Awake()
    {
        _buttons = new List<ColorButton>();
    }

    public void Init(ModelView modelView)
    {
        if(_currentModelView != null)
        {
            _currentModelView.SetDefaultColor();
            _currentModelView = null;
        }

        if(_buttons.Count > 0)
        {
            foreach (ColorButton button in _buttons)
            {
                Destroy(button.gameObject);
            }

            _buttons.Clear();
        }

        _currentModelView = modelView;
        int count = _currentModelView.GetCountOfColors();

        for (int i = 0; i < count; i++)
        {
            ColorButton button = Instantiate(_buttonPrefab, _container).GetComponent<ColorButton>();
            button.Init(i);
            button.Click += OnButtonClick;
            button.ButtonDisabled -= OnButtonDisabled;
            ButtonView buttonView = button.GetComponent<ButtonView>();
            Color color = _currentModelView.GetColor(i);
            buttonView.SetColor(color);
            _buttons.Add(button);
        }
    }

    private void OnButtonClick(int index)
    {
        _currentModelView.SetColor(index);
    }

    private void OnButtonDisabled(ColorButton button)
    {
        button.Click -= OnButtonClick;
        button.ButtonDisabled -= OnButtonDisabled;
    }
}
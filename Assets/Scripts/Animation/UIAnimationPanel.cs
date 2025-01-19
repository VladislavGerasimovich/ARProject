using System.Collections.Generic;
using UnityEngine;

public class UIAnimationPanel : MonoBehaviour
{
    [SerializeField] private Transform _buttonPrefab;
    [SerializeField] private Transform _container;

    private List<AnimationButton> _buttons;
    private ModelAnimations _currentModelAnimations;

    private void Awake()
    {
        _buttons = new List<AnimationButton>();
    }

    public void Init(ModelAnimations modelAnimations)
    {
        if (_buttons.Count > 0)
        {
            foreach (AnimationButton button in _buttons)
            {
                Destroy(button.gameObject);
            }

            _buttons.Clear();
        }

        _currentModelAnimations = modelAnimations;
        int count = modelAnimations.GetCount();
        
        for (int i = 0; i < count; i++)
        {
            AnimationButton button = Instantiate(_buttonPrefab, _container).GetComponent<AnimationButton>();
            button.Init(i);
            button.Click += OnButtonClick;
            button.ButtonDisabled += OnButtonDisabled;
            ButtonText buttonText = button.GetComponent<ButtonText>();
            string name = _currentModelAnimations.GetNameOfAnimation(i);
            buttonText.Set(name);
            _buttons.Add(button);
        }
    }

    private void OnButtonClick(int index)
    {
        _currentModelAnimations.SetAnimation(index);
    }

    private void OnButtonDisabled(AnimationButton button)
    {
        button.Click -= OnButtonClick;
        button.ButtonDisabled -= OnButtonDisabled;
    }
}
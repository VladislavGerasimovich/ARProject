using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private List<ItemData> _itemsData;
    [SerializeField] private ObjectPlacer _objectPlacer;
    [SerializeField] private ItemView _itemTemplate;
    [SerializeField] private Transform _container;
    [SerializeField] private UIColorPanel _colorPanel;
    [SerializeField] private UIAnimationPanel _animationPanel;

    private void Start()
    {
        foreach (ItemData itemData in _itemsData)
        {
            AddItem(itemData);
        }
    }

    private void AddItem(ItemData itemData)
    {
        ItemData item = itemData;
        ItemView itemView = Instantiate(_itemTemplate, _container);
        itemView.SetView(item);
        ItemButton button = itemView.GetComponent<ItemButton>();
        button.Init(item);
        button.Click += OnButtonClick;
        button.ButtonDisabled += OnButtonDisabled;
    }

    private void OnButtonClick(ItemData itemData)
    {
        ItemData item = itemData;
        _objectPlacer.SetInstalledObject(item, out GameObject installedObject);
        ModelView modelView = installedObject.GetComponent<ModelView>();
        ModelAnimations modelAnimations = installedObject.GetComponent<ModelAnimations>();
        _colorPanel.Init(modelView);
        _animationPanel.Init(modelAnimations);
    }

    private void OnButtonDisabled(ItemButton itemButton)
    {
        itemButton.Click -= OnButtonClick;
        itemButton.ButtonDisabled -= OnButtonDisabled;
    }
}
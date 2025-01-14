using System.Collections.Generic;
using UnityEngine;

public class SelectionPanel : MonoBehaviour
{
    [SerializeField] private List<ItemData> _itemsData;
    [SerializeField] private Transform _objectPlacer;
    [SerializeField] private ItemView _itemTemplate;
    [SerializeField] private Transform _container;

    private void Start()
    {
        foreach (ItemData itemData in _itemsData)
        {
            AddItem(itemData);
        }
    }

    private void AddItem(ItemData itemData)
    {
        ItemView itemView = Instantiate(_itemTemplate, _container);
        itemView.SetView(itemData);
        ItemButton button = itemView.GetComponent<ItemButton>();
        button.Init(itemData);
        button.Click += OnButtonClick;  
        button.ButtonDisabled += OnButtonDisabled;
    }

    private void OnButtonClick(ItemData itemData)
    {

    }

    private void OnButtonDisabled(ItemButton itemButton)
    {
        itemButton.Click -= OnButtonClick;
        itemButton.ButtonDisabled -= OnButtonDisabled;
    }
}
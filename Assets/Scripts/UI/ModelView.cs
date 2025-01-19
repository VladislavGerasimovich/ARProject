using System.Collections.Generic;
using UnityEngine;

public class ModelView : MonoBehaviour
{
    [SerializeField] private List<Color> _colors;
    [SerializeField] private MeshRenderer _renderer;

    private Color _defaultColor;

    private void Awake()
    {
        _defaultColor = new Color(_renderer.sharedMaterial.color.r, _renderer.sharedMaterial.color.g, _renderer.sharedMaterial.color.b);
    }

    public void SetColor(int index)
    {
        _renderer.sharedMaterial.color = _colors[index];
    }

    public void SetDefaultColor()
    {
        _renderer.sharedMaterial.color = _defaultColor;
    }

    public int GetCountOfColors()
    {
        return _colors.Count;
    }

    public Color GetColor(int index)
    {
        return _colors[index];
    }
}
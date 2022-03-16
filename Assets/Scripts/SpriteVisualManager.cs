using System;
using TMPro;
using UnityEngine;

public class SpriteVisualManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private TMP_Text tmpTextLabel;

    [NonSerialized]
    public Color Color;

    [NonSerialized]
    public Color PressedColor;

    [NonSerialized]
    public string Label;

    [NonSerialized]
    public float Width = 1;

    private bool _isPressed = false;

    public bool IsPressed
    {
        get => _isPressed;
        set
        {
            spriteRenderer.color = value ? PressedColor : Color;
            _isPressed = value;
        }
    }

    private void Start()
    {
        SetupVisual();
    }

    private void SetupVisual()
    {
        spriteRenderer.color = Color;
        spriteRenderer.size = new Vector2(Width, spriteRenderer.size.y);
        if (tmpTextLabel != null)
        {
            tmpTextLabel.text = Label;
        }
    }
}
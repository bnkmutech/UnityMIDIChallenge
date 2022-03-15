using TMPro;
using UnityEngine;

public class SpriteVisualManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private TMP_Text tmpTextLabel;

    [SerializeField]
    private Color color;

    [SerializeField]
    private string label;

    private void OnValidate()
    {
        SetupVisual();
    }

    private void Start()
    {
        SetupVisual();
    }

    private void SetupVisual()
    {
        spriteRenderer.color = color;
        if (tmpTextLabel != null)
        {
            tmpTextLabel.text = label;
        }
    }
}
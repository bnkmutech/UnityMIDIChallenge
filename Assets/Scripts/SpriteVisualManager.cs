using TMPro;
using UnityEngine;

public class SpriteVisualManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private TMP_Text tmpTextLabel;

    [SerializeField]
    public Color color;

    [SerializeField]
    public string label;

    [SerializeField]
    public float width = 1;

    private void Start()
    {
        SetupVisual();
    }

    private void SetupVisual()
    {
        spriteRenderer.color = color;
        spriteRenderer.size = new Vector2(width, spriteRenderer.size.y);
        if (tmpTextLabel != null)
        {
            tmpTextLabel.text = label;
        }
    }
}
using UnityEngine;

public class KeyPanelManager : MonoBehaviour
{
    [SerializeField]
    private SpriteVisualManager spriteVisualManager;

    public void ButtonDown()
    {
        spriteVisualManager.IsPressed = true;
    }

    public void ButtonUp()
    {
        spriteVisualManager.IsPressed = false;
    }
}
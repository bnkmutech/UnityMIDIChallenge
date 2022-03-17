using UnityEngine;

public class KeyPanelController : MonoBehaviour
{
    [SerializeField]
    private VisualController visualController;

    public void ButtonDown()
    {
        visualController.IsPressed = true;
    }

    public void ButtonUp()
    {
        visualController.IsPressed = false;
    }
}
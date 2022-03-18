using UnityEngine;
using UnityEngine.Events;

public class KeyPanelController : MonoBehaviour
{
    [SerializeField]
    private VisualController visualController;

    private NoteController _currentNote;
    public UnityEvent addScore = new UnityEvent();

    public void ButtonDown()
    {
        visualController.IsPressed = true;
        if (_currentNote != null)
        {
            addScore.Invoke();
            _currentNote.OnKeyHit();
        }
    }

    public void ButtonUp()
    {
        visualController.IsPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out NoteController noteController))
        {
            _currentNote = noteController;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _currentNote = null;
    }
}
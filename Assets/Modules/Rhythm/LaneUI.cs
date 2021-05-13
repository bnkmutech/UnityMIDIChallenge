using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class LaneUI : MonoBehaviour
{
    public string keyText
    {
        set => _keyText.text = value;
    }

    public Color laneColor => _laneColor;
    public RectTransform spawnPoint => _spawnPoint;
    public RectTransform despawnPoint => _despawnPoint;

    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private RectTransform _spawnPoint;
    [SerializeField] private RectTransform _despawnPoint;
    [SerializeField] private Image _laneColorImage;
    [SerializeField] private Image _noteHitImage;
    [SerializeField] private CanvasGroup _activeAnimation;
    [SerializeField] private CanvasGroup _activeFader;
    [SerializeField] private GameObject _noteHitEffect;

    private NoteWithOctave[] _notes;
    private Color _laneColor;
    
    private Sequence _activeAnimationSequence;
    private Sequence _noteHitSequence;
    private Tween _activeTween;
    
    public void Initialize(Color laneColor, NoteWithOctave[] notes)
    {
        _laneColor = laneColor;
        _laneColorImage.color = laneColor;
        _noteHitImage.color = laneColor;
        _notes = notes;
        var sequence = DOTween.Sequence();
        sequence.Append(_activeAnimation.DOFade(0, 0.25f));
        sequence.Append(_activeAnimation.DOFade(1, 0.25f));
        _activeAnimationSequence = sequence;
        _activeAnimationSequence.Play();
    }
    public bool HasNote(string noteName)
    {
        return _notes.Any(note => note.IsMatch(noteName));
    }
    public void OnPressed(InputAction.CallbackContext context)
    {
        _activeTween?.Kill();
        _activeTween = _activeFader.DOFade(1, 0.15f).SetEase(Ease.InOutCirc);
    }
    public void OnReleased(InputAction.CallbackContext context)
    {
        _activeTween?.Kill();
        _activeTween = _activeFader.DOFade(0.7f, 0.15f).SetEase(Ease.InOutCirc);
    }

    public async void OnNoteHit()
    {
        _noteHitEffect.SetActive(true);

        await Task.Delay(150);
        
        if (_noteHitEffect.activeInHierarchy)
            _noteHitEffect.SetActive(false);
    }
    public class Factory : PlaceholderFactory<Object, Transform, LaneUI>
    {
        
    }
}
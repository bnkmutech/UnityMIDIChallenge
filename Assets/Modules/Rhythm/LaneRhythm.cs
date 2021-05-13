using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LaneRhythm : MonoBehaviour
{
    public bool isEnabled => _isEnabled;
    public string id => _id;
    
    [SerializeField] private bool _isEnabled;
    [SerializeField] private string _id;
    [SerializeField] private Image _rhythmColorImage;

    private Tween _tween;
    private RectTransform _rect;

    public void Initialize(string id)
    {
        _id = id;
        _rect = GetComponent<RectTransform>();
    }
    public void Initialize(string id, Vector3 newPos)
    {
        _id = id;
        _tween?.Kill();
        _rect.transform.position = newPos;
    }
    public void SetColor(Color laneColor)
    {
        _rhythmColorImage.color = laneColor;
    }
    public bool IsMatch(string id)
    {
        return _id == id;
    }
    public void SetEnable(bool enabled)
    {
        _tween?.Kill();
        _isEnabled = enabled;
        gameObject.SetActive(_isEnabled);
    }

    public void Pause()
    {
        if (_tween.IsPlaying())
            _tween.Pause();
        else
            _tween.Play();
    }
    public void Move(float delay, Vector2 desiredPosition)
    {
        var distanceY = Mathf.Abs(_rect.anchoredPosition.y - desiredPosition.y);
        var yAmountPerSeconds = distanceY / delay;
        var finalMoveYAmount = desiredPosition.y - yAmountPerSeconds;

        _tween?.Kill();
        _tween = _rect.DOLocalMoveY(finalMoveYAmount, delay + 1f).SetEase(Ease.Linear).OnComplete(() =>
        {
            _isEnabled = false;
            gameObject.SetActive(_isEnabled);
        });
    }

    public class Factory : PlaceholderFactory<LaneRhythm, Vector2, Transform, LaneRhythm>
    {
        
    }
}
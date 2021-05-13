using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuController : IInitializable, ITickable
{
    [Inject] private SignalBus _signalBus;
    [Inject] private GameMenuSettings _settings;
    [Inject] private PlaceholderFactory<TrackButton> _buttonFactory;

    private bool _isPause;
    private bool _isEnabled;
    private bool _isTrackStart;
    private List<TrackButton> _trackButtons = new List<TrackButton>();

    public void Initialize()
    {
        _settings.gamePauseMenu.resumeOnClick = PauseHandle;
        _settings.gamePauseMenu.selectTrackOnClick = () =>
        {
            _settings.gamePauseMenu.gameObject.SetActive(false);
            _signalBus.Fire(new GameEndSignal());
        };
        _settings.gamePauseMenu.quitGameOnClick = Application.Quit;
        
        for (int i = 0; i < _settings.tracks.Length; i++)
        {
            var track = _settings.tracks[i];
            var trackButton = _buttonFactory.Create();
            trackButton.onclick = (() =>
            {
                _isEnabled = true;
                _settings.trackMenu.gameObject.SetActive(false);
                _signalBus.Fire(new TrackSelectSignal(track.difficulty, track));
            });
            trackButton.transform.SetParent(_settings.trackButtonSlot);
            trackButton.transform.localScale = Vector3.one;
            trackButton.SetText(_settings.tracks[i]);
            _trackButtons.Add(trackButton);
        }
    }
    public void Tick()
    {
        if (!_isEnabled)
            return;
        
        if (Input.GetKeyUp(_settings.pauseKey))
            PauseHandle();
    }
    public async void OnTrackStarted(TrackStartedSignal signal)
    {
        _isTrackStart = true;
        await Task.Delay(150);
        _settings.pauseHint.SetActive(true);
    }
    public async void OnGameEndSignal(GameEndSignal signal)
    {
        _isEnabled = false;
        _settings.pauseHint.SetActive(false);
        _settings.trackMenu.gameObject.SetActive(true);
    }
    private async void PauseHandle()
    {
        if (!_isTrackStart)
            return;
        
        _isPause = !_isPause;

        if (!_isPause)
        {
            var countDown = 3;
            while (countDown > 0)
            {
                _settings.gamePauseMenu.SetTime(countDown);
                await Task.Delay(1000);
                countDown -= 1;
            }
            
            _settings.gamePauseMenu.countDownGameObject.SetActive(false);
        }
        
        _settings.gamePauseMenu.SetActive(_isPause);
        _signalBus.Fire(new GamePauseSignal(_isPause));

    }
}
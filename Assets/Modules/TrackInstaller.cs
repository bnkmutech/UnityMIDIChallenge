using UnityEngine;
using Zenject;

public class TrackInstaller : MonoInstaller<TrackInstaller>
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private TrackSettings _settings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TrackController>().AsSingle();
        Container.BindInterfacesAndSelfTo<MidiWorker>().AsSingle();
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        Container.Bind<TrackSettings>().FromInstance(_settings).AsSingle();
        
        Container.DeclareSignal<RhythmPalette>();
        Container.DeclareSignal<TrackStartedSignal>();
        
        Container.BindSignal<TrackSelectedSignal>().ToMethod<TrackController>(c => c.OnTrackSelected).FromResolve();
        Container.BindSignal<GamePauseSignal>().ToMethod<TrackController>(c => c.OnGamePause).FromResolve();
    }
}
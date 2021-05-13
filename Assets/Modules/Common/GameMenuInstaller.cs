using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameMenuInstaller : MonoInstaller<GameMenuInstaller>
{
    [SerializeField] private GameMenuSettings _settings;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameMenuController>().AsSingle();
        Container.BindFactory<TrackButton, PlaceholderFactory<TrackButton>>().FromComponentInNewPrefab(_settings.trackButtonPrefab).AsSingle();
        Container.Bind<GameMenuSettings>().FromInstance(_settings).AsSingle();

        Container.BindSignal<TrackStartedSignal>().ToMethod<GameMenuController>(c => c.OnTrackStarted).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<GameMenuController>(c => c.OnGameEndSignal).FromResolve();
    }
}
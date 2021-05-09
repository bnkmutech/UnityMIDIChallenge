using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Create GameInstaller", fileName = "GameInstaller", order = 0)]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private List<GameMode> _modes = new List<GameMode>();
    public override void InstallBindings()
    {
        Container.Bind<GameModeController>().AsSingle();
        Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
        Container.Bind<List<GameMode>>().FromInstance(_modes).AsSingle();

        Container.DeclareSignal<GamePauseSignal>();
        Container.DeclareSignal<GameEndSignal>();
        Container.DeclareSignal<TrackSelectSignal>();
        Container.DeclareSignal<TrackSelectedSignal>();
        
        Container.BindSignal<TrackSelectSignal>().ToMethod<GameModeController>(c => c.OnTrackSelect)
            .FromResolve();
    }
}
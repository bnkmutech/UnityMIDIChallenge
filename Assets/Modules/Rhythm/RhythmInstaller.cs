using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RhythmInstaller : MonoInstaller<RhythmInstaller>
{
    [SerializeField] private GameBoardUI _gameBoardUI;
    public override void InstallBindings()
    {
        //Note: Controllers and Workers
        Container.BindInterfacesAndSelfTo<RhythmGameController>().AsSingle();
        Container.BindInterfacesAndSelfTo<RhythmLanePresenter>().AsSingle();
        Container.Bind<RhythmLaneInputWorker>().AsSingle();
        Container.Bind<RhythmPaletteSpawnWorker>().AsSingle();
        Container.Bind<RhythmPalettePool>().AsSingle();
        Container.Bind<LaneInputControls>().AsSingle();
        
        //Note: Factory to spawn mono objects in scene
        Container.BindFactory<Object, Transform, LaneUI, LaneUI.Factory>().FromFactory<LaneUIFactory>();
        Container.BindFactory<LaneRhythm, Vector2, Transform, LaneRhythm, LaneRhythm.Factory>().FromFactory<LaneRhythmFactory>();
        Container.BindFactory<GameBoardUI, GameBoardUI.Factory>().FromComponentInNewPrefab(_gameBoardUI).AsSingle();

        //Note: Event Signals Wiring
        Container.DeclareSignal<RhythmPalette>();
        Container.DeclareSignal<NoteInteractSignal>();

        Container.BindSignal<GameEndSignal>().ToMethod<RhythmLanePresenter>(p => p.OnGameEnd).FromResolve();
        Container.BindSignal<GameEndSignal>().ToMethod<RhythmGameController>(c => c.OnGameEnd).FromResolve();
        Container.BindSignal<GamePauseSignal>().ToMethod<RhythmGameController>(c => c.OnGamePause).FromResolve();
        Container.BindSignal<GamePauseSignal>().ToMethod<RhythmLanePresenter>(p => p.OnGamePause).FromResolve();
        Container.BindSignal<TrackSelectedSignal>().ToMethod<RhythmGameController>(c => c.OnTrackSelected).FromResolve();
        Container.BindSignal<TrackSelectedSignal>().ToMethod<RhythmLanePresenter>(p => p.OnTrackSelected).FromResolve();
        Container.BindSignal<RhythmPalette>().ToMethod<RhythmGameController>(c => c.OnNoteDetected).FromResolve();
        Container.BindSignal<RhythmPalette>().ToMethod<RhythmLanePresenter>(c => c.OnNoteDetected).FromResolve();
        Container.BindSignal<NoteInteractSignal>().ToMethod<RhythmLanePresenter>(p => p.OnNoteInteract).FromResolve();
    }
}
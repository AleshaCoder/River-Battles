using UnityEngine;

public class LoadGameState : IPayloadedState<string>
{
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadGameState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter(string sceneName)
    {
        _loadingCurtain.Show();
        _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
        Debug.Log("Game Init");
        GameObject.FindObjectOfType<LevelsPool>().Init();
        Services.Container.Single<LevelLoader>().LoadCurrentLevel();
        Services.Container.Single<RunnerMovement>().Init();
        ScreenManager.ShowMain();
        CameraFollow();

        _stateMachine.Enter<GameLoopState>();        
    }

    private void CameraFollow()
    {
        Services.Container.Single<CameraFollower>().Init();
        Services.Container.Single<CameraFollower>().Follow(x: true, y: true, z: true, useRotation: true);
    }
}

public class LoadLevelState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly LoadingCurtain _loadingCurtain;

    public LoadLevelState(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain)
    {
        _stateMachine = gameStateMachine;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter()
    {
        _loadingCurtain.Show();
        Services.Container.Single<RunnerMovement>().MoveToStartPosition();
        Services.Container.Single<CameraFollower>().MoveToStartPosition();
        Services.Container.Single<CameraFollower>().Follow(x: true, y: true, z: true, useRotation: true);
        Services.Container.Single<LevelLoader>().LoadNextLevel();
        StackedBoatsPool.Instance.Clear();
        StackedWarriorPool.Instance.Clear();
        ScreenManager.ShowMain();
        Debug.Log("New Level");
        _stateMachine.Enter<GameLoopState>();        
    }

    public void Exit() =>
      _loadingCurtain.Hide();
}

public class ReloadLevel : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly LoadingCurtain _loadingCurtain;

    public ReloadLevel(GameStateMachine gameStateMachine, LoadingCurtain loadingCurtain)
    {
        _stateMachine = gameStateMachine;
        _loadingCurtain = loadingCurtain;
    }

    public void Enter()
    {
        _loadingCurtain.Show();
        Services.Container.Single<RunnerMovement>().MoveToStartPosition();
        Services.Container.Single<CameraFollower>().MoveToStartPosition();
        Services.Container.Single<CameraFollower>().Follow(x: true, y: true, z: true, useRotation: true);
        StackedBoatsPool.Instance.Clear();
        StackedWarriorPool.Instance.Clear();
        Services.Container.Single<LevelLoader>().ReloadLevel();
        ScreenManager.ShowMain();
        Debug.Log("New Level");
        _stateMachine.Enter<GameLoopState>();
    }

    public void Exit() =>
      _loadingCurtain.Hide();
}

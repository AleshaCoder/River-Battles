using UnityEngine;

public class BootstrapState : IState
{
    private const string Initial = "StartScene";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
    }

    public void Enter()
    {
        EnterLoadLevel();
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel()
    {
        _stateMachine.Enter<LoadGameState, string>("18.04 Demo");
    }
}

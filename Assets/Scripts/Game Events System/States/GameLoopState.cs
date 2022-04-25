﻿using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLoopState : IState
{
    private GameStateMachine _stateMachine;
    private GameStarter _gameStarter;

    public GameLoopState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        Services.Container.Single<GameStarter>().Stop();
        Services.Container.Single<GameStarter>().enabled = false;
        GameStarter.OnGameStarted -= Services.Container.Single<RunnerMovement>().StartMoving;
        GameStarter.OnGameStarted -= () => Services.Container.Single<CameraFollower>().DistanceToTarget =40;

        Services.Container.Single<Island>().OnIsland += () => _stateMachine.Enter<IslandAttackState>();
    }

    public void Enter()
    {
        Services.Container.Single<CameraFollower>().DistanceToTarget *= 2;
        _gameStarter = Services.Container.Single<GameStarter>();
        _gameStarter.enabled = true;
        GameStarter.OnGameStarted += Services.Container.Single<RunnerMovement>().StartMoving;
        GameStarter.OnGameStarted += () => Services.Container.Single<CameraFollower>().DistanceToTarget = 40; 

        GameObject.FindObjectOfType<SurfaceBoat>().Animate();

        Services.Container.Single<Island>().OnIsland += () =>_stateMachine.Enter<IslandAttackState>();

        Debug.Log("GameLoopState");
    }
}

public class IslandAttackState : IState
{
    private GameStateMachine _stateMachine;
    private CameraFollower _cameraFollower;
    public IslandAttackState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
        _cameraFollower.DistanceToTarget *= 2;
        ScreenManager.OnLose -= () => _stateMachine.Enter<ReloadLevel>();
        ScreenManager.OnWin -= () => _stateMachine.Enter<LoadLevelState>();
    }

    public void Enter()
    {
        _cameraFollower = Services.Container.Single<CameraFollower>();

        _cameraFollower.Follow(false, true, true, true, _cameraFollower.DistanceToTarget/2);
        Services.Container.Single<RunnerMovement>().Stop();
        ScreenManager.OnLose += () => _stateMachine.Enter<ReloadLevel>();
        ScreenManager.OnWin += () => _stateMachine.Enter<LoadLevelState>();
        Debug.Log("IslandAttackState");
    }
}

public class LoseState : IState
{
    private GameStateMachine _stateMachine;

    public LoseState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
        //string json = JsonConvert.SerializeObject(GameObject.FindObjectOfType<LevelsPool>().CurrentLevel.EndLevel);
        //Debug.Log(json);
        //AppMetrica.Instance.ReportEvent("level_end", json);
        //AppMetrica.Instance.SendEventsBuffer();

        Debug.Log("LoseState");
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Services: MonoBehaviour
{
    [SerializeField] private CameraFollower _cameraFollower;
    [SerializeField] private RunnerMovement _runnerMovement;
    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private LevelLoader _levelLoader;

    private static Services _instance;
    public static Services Container => _instance;

    public void RegisterSingle<TService>(TService implementation) where TService : IService =>
      Implementation<TService>.ServiceInstance = implementation;

    public TService Single<TService>() where TService : IService =>
      Implementation<TService>.ServiceInstance;

    private class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Container.RegisterSingle<CameraFollower>(_cameraFollower);
            Container.RegisterSingle<RunnerMovement>(_runnerMovement);
            Container.RegisterSingle<GameStarter>(_gameStarter);
            Container.RegisterSingle<LevelLoader>(_levelLoader);
        }
    }
}

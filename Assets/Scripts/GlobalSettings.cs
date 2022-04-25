using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    [Header("Battle Settings")]
    [SerializeField] private int _shotsCountPerUnit = 3;
    [SerializeField] private float _bulletSpeed = 100;
    [SerializeField] private float _secondsBetweenShots = 50;
    [SerializeField] private float _secondsBetweenKillings = 50;
    [SerializeField] private float _secondsAfterBattleStartForKillingsUnits = 1000;

    public static GlobalSettings Settings;

    public int ShotsCountPerUnit => _shotsCountPerUnit;
    public float BulletSpeed => _bulletSpeed;
    public float SecondsBetweenShots => _secondsBetweenShots;
    public float SecondsBetweenKillings => _secondsBetweenKillings;
    public float SecondsAfterBattleStartForKillingsUnits => _secondsAfterBattleStartForKillingsUnits;

    public void Init()
    {
        Settings = this;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Island : MonoBehaviour, IService
{
    [SerializeField] private BoatChecker _boatChecker;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<Point> _warriorPositions;
    [SerializeField] private List<Point> _dockingPoints;
    [SerializeField] private ShoreDocking _shoreDocking;
    [SerializeField] private MoveWarriorsToIsland _moveWarriorsToIsland;

    public Action OnIsland;

    public void Attack(int count)
    {
        Fire();
        Kill(count);
    }

    public int GetEnemiesCount()
    {
        return _enemies.Count;
    }

    public List<Enemy> GetEnemies()
    {
        return _enemies;
    }

    public async Task Fire()
    {
        var warriors = StackedWarriorPool.Instance.GetWarriors();

        for (int i = 0; i < 3; i++)
        {
            foreach (var item in _enemies)
            {
                item.Gun.Shot(warriors[UnityEngine.Random.Range(0, warriors.Count)].transform);
                await Task.Delay(UnityEngine.Random.Range(50, 100));
            }
        }
    }

    public async Task Kill(int count)
    {
        await Task.Delay(2000);
        var enemiesCount = _enemies.Count;
        for (int i = 0; i < enemiesCount; i++)
        {
            count--;
            _enemies[0].Kill();
            _enemies.RemoveAt(0);
            await Task.Delay(200);
            if (count == 0)
                break;
        }
        if (_enemies.Count == 0)
        {
            ScreenManager.ShowWin();
        }
        else
        {
            ScreenManager.ShowLose();
        }
    }


    private void OnEnable()
    {
        Services.Container.RegisterSingle(this);
        _boatChecker.OnFindingBoat += (Boat boat) => OnIsland?.Invoke();
        _boatChecker.OnFindingBoat += StackBoats;
    }

    private void StackBoats(Boat boat)
    {
        _shoreDocking.Animate(_dockingPoints);
        _shoreDocking.OnAnimationEnd += () => _moveWarriorsToIsland.Animate(this, _warriorPositions);
        _shoreDocking.OnAnimationEnd += () => _shoreDocking.OnAnimationEnd = null;
        _moveWarriorsToIsland.OnAnimationEnd += () => boat.Attack(this);
        _moveWarriorsToIsland.OnAnimationEnd += () => _moveWarriorsToIsland.OnAnimationEnd = null;
    }

    private void OnDisable()
    {
        _boatChecker.OnFindingBoat -= (Boat boat) => OnIsland?.Invoke();
        _boatChecker.OnFindingBoat -= StackBoats;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBoat : MonoBehaviour
{
    [SerializeField] private BoatChecker _boatChecker;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _extraBoats;
    [SerializeField] private float _followSpeed = 1f;

    public Action OnDestroy;

    private void OnEnable()
    {
        _boatChecker.OnFindingBoat += AttackBoats;
    }

    private void OnDisable()
    {
        _boatChecker.OnFindingBoat -= AttackBoats;
    }

    private void AttackBoats(Boat attacked)
    {
        if (_targetFollower != null)
            _targetFollower.Follow(attacked.transform, true, false, true, -_followSpeed, 1);
        attacked.Attack(this);
    }

    public int GetEnemiesCount()
    {
        return _enemies.Count;
    }

    public List<Enemy> GetEnemies()
    {
        return _enemies;
    }

    public void Attack(int attackingCount)
    {
        var t = Fire();
        var t1 = Kill(attackingCount);
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
            _targetFollower.StopFollow();
            Destroy(gameObject, 2);
            foreach (var item in _extraBoats)
            {
                Destroy(item.gameObject);
            }
            await Task.Delay(TimeSpan.FromSeconds(2));
            OnDestroy?.Invoke();
        }
    }
}

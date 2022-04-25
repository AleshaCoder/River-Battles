using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Boat : MonoBehaviour, IStackable
{
    [SerializeField] private BoatChecker _boatChecker;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private Point _dockingPoint;
    [SerializeField] private Point _forwardPoint;

    private bool _inStack = false;

    public Point GetDockingPoint()
    {
        return _dockingPoint;
    }

    public void StackSelf(Point dockingPoint)
    {
        var localPosForward = _forwardPoint.transform.localPosition;
        var directionX = transform.position.x - _forwardPoint.Position.x;
        var directionZ = transform.position.z - _forwardPoint.Position.z;

        _forwardPoint.SetPosition(dockingPoint);
        transform.position = new Vector3(_forwardPoint.Position.x + directionX, transform.position.y, _forwardPoint.Position.z + directionZ);
        _forwardPoint.transform.localPosition = localPosForward;

        _targetFollower.Follow(dockingPoint.transform.parent, true, true, true, 10, -1);
        _inStack = true;
        _boatChecker.SwitchOn();
    }

    public void Unstack()
    {
        _boatChecker.SwitchOn();
        _targetFollower.StopFollow();
        _inStack = false;
    }

    public void StackToSelf(IStackable boat)
    {
        if (boat.InStack() == true)
            return;
        boat.StackSelf(_dockingPoint);
    }

    public bool InStack()
    {
        return _inStack;
    }

    public void Stop()
    {
        _targetFollower.StopFollow();
    }

    public void Attack(EnemyBoat enemyBoat)
    {
        enemyBoat.Attack(StackedWarriorPool.Instance.GetWarriorsCount());
        var t = Fire(enemyBoat.GetEnemies());
        var t1 = Kill(enemyBoat.GetEnemiesCount());
    }

    public void Attack(Island island)
    {
        island.Attack(StackedWarriorPool.Instance.GetWarriorsCount());
        var t = Fire(island.GetEnemies());
        var t1 = Kill(island.GetEnemiesCount());
    }

    public async Task Fire(List<Enemy> enemies)
    {
        var warriors = StackedWarriorPool.Instance.GetWarriors();
        for (int i = 0; i < 3; i++)
        {
            foreach (var item in warriors)
            {
                item.Gun.Shot(enemies[Random.Range(0, enemies.Count)].transform);
                await Task.Delay(Random.Range(50,100));
            }
        }        
    }

    public async Task Kill(int count)
    {
        await Task.Delay(2000);
        for (int i = 0; i < StackedWarriorPool.Instance.GetWarriorsCount(); i++)
        {
            count--;
            StackedWarriorPool.Instance.TryKill();
            await Task.Delay(200);
            if (count == 0)
                break;
        }
    }

    private void OnEnable()
    {
        _boatChecker.OnFindingBoat += (Boat boat) => StackedBoatsPool.Instance.StackBoat(boat);
    }

    private void OnDisable()
    {
        _boatChecker.OnFindingBoat -= (Boat boat) => StackedBoatsPool.Instance.StackBoat(boat);
    }
}

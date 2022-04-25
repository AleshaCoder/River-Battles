using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MoveWarriorsToIsland : MonoBehaviour
{
    public Action OnAnimationEnd;
    public async void Animate(Island island, List<Point> dockingPoints)
    {
        var warriors = StackedWarriorPool.Instance.GetWarriors();
        var enemies = island.GetEnemies();
        for (int i = 0; i < warriors.Count; i++)
        {
            warriors[i].StackSelf(dockingPoints[i], true, false);
        }
        await Task.Delay(2000);
        //for (int i = 0; i < warriors.Count; i++)
        //{
        //    if (i >= enemies.Count)
        //        warriors[i].transform.LookAt(enemies[enemies.Count - 1].transform);
        //    else
        //        warriors[i].transform.LookAt(enemies[i].transform);
        //}
        OnAnimationEnd?.Invoke();
    }
}

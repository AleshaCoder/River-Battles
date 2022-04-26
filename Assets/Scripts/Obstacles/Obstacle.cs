using System.Collections;
using System.Collections.Generic;
using Obstacles;
using UnityEngine;

public class Obstacle : MonoBehaviour, IObstacleTryReciever
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IThrowable throwable))
        {
            TryObstacleRecieve(throwable);
        }
    }

    public async void TryObstacleRecieve(IThrowable throwable)
    {
        await throwable.Throw();
        Destroy(this.gameObject);
    }
}

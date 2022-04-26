using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Obstacles
{


public interface IObstacleTryReciever
{
    void TryObstacleRecieve(IThrowable throwable);
}
}

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShoreDocking : MonoBehaviour
{
    public Action OnAnimationEnd;
    public void Animate(List<Point> dockingPoints)
    {
        var boats = StackedBoatsPool.Instance.GetBoats();
        var sequence = DOTween.Sequence();
        sequence.Append(boats[0].transform.DOMoveX(dockingPoints[0].Position.x, 1));
        sequence.Insert(0, boats[0].transform.DOMoveZ(dockingPoints[0].Position.z, 1));
        sequence.Insert(0, boats[0].transform.DORotate(new Vector3(0, -90, 0), 1));

        for (int i = 1; i < dockingPoints.Count; i++)
        {
            if (i == boats.Count)
                break;
            boats[i].Stop();
            sequence.Insert(0, boats[i].transform.DOMoveX(dockingPoints[i].Position.x, 1));
            sequence.Insert(0, boats[i].transform.DOMoveZ(dockingPoints[i].Position.z, 1));
        }
        sequence.Play();
        sequence.onComplete += () => OnAnimationEnd?.Invoke();
        sequence.onComplete += () => sequence.onComplete = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorGroup : MonoBehaviour
{
    [SerializeField] private Vector3 _defaultAngle;
    [SerializeField] private List<Point> _warriorsPositions;

    private List<Warrior> _warriors;

    private void Awake()
    {
        _warriors = new List<Warrior>();
    }

    public int SendWarriors(List<Warrior> warriors)
    {
        Debug.Log("Send warriors " + warriors.Count);
        if (warriors.Count == 0)
            return 0;

        for (int i = 0; i < _warriorsPositions.Count; i++)
        {
            if (_warriorsPositions[i].InUse == true)
                continue;
            warriors[0].StackSelf(_warriorsPositions[i]);
            StackedWarriorPool.Instance.Add(warriors[0]);
            _warriors.Add(warriors[0]);
            warriors.RemoveAt(0);
            if (warriors.Count == 0)
                return 0;
        }

        return warriors.Count;
    }
}

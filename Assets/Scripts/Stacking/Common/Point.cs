using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private bool _inUse;
    public Vector3 Position => transform.position;
    public Vector3 Rotation => transform.localEulerAngles;
    public bool InUse => _inUse;

    public void Take()
    {
        _inUse = true;
    }

    public void Free()
    {
        _inUse = false;
    }

    public void SetPosition(Point point)
    {
        transform.position = point.Position;
    }
}

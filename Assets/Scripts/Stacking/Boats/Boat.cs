using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Boat : MonoBehaviour, IStackable
{
    [SerializeField] private BoatChecker _boatChecker;
    [SerializeField] private TargetFollower _targetFollower;
    [SerializeField] private Point _dockingPoint;
    [SerializeField] private Point _forwardPoint;

    private bool _inStack = false;
    private Boat _nextBoat;


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

        _targetFollower.Follow(dockingPoint.transform.parent, true, true, true, 20, -1);
        _inStack = true;
        _boatChecker.SwitchOn();
    }

    public void Unstack()
    {
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
}

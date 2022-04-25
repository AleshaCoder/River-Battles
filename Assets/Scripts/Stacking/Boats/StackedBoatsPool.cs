using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedBoatsPool : StackedObjectPool
{
    [SerializeField] private Boat _firstBoat;
    [SerializeField] private CameraFollower _cameraFollower;

    public static StackedBoatsPool Instance { get; private set; }

    public List<Boat> GetBoats()
    {
        List<Boat> boats = new List<Boat>();
        foreach (var item in _stackables)
        {
            boats.Add((Boat)item);
        }
        return boats;
    }

    public void StackBoat(Boat boat)
    {       
        _stackables[_stackables.Count - 1].StackToSelf(boat);
        Add(boat);
        _cameraFollower.DistanceToTarget += 10;
    }

    public void Clear()
    {
        foreach (var item in _stackables)
        {
            item.Unstack();
        }
        _stackables.Clear();
        _stackables.Add(_firstElement);
    }

    public override void Awake()
    {
        if (Instance == null)
            Instance = this;
        _firstElement = _firstBoat;
        base.Awake();        
    }
}

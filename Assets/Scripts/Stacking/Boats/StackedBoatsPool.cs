using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedBoatsPool : StackedObjectPool
{
    [SerializeField] private protected Boat _firstBoat;
    public static StackedBoatsPool Instance { get; private set; }

    public void StackBoat(Boat boat)
    {
        _stackables[_stackables.Count - 1].StackToSelf(boat);
        Add(boat);
    }

    public override void Awake()
    {
        if (Instance == null)
            Instance = this;
        _firstElement = _firstBoat;
        base.Awake();        
    }
}

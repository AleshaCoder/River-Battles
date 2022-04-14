using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IStackable
{
    public Point GetDockingPoint() => throw new System.NotImplementedException();
    public bool InStack() => throw new System.NotImplementedException();
    public void StackSelf(Point point) => throw new System.NotImplementedException();
    public void StackToSelf(IStackable stackable) => throw new System.NotImplementedException();
    public void Unstack() => throw new System.NotImplementedException();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

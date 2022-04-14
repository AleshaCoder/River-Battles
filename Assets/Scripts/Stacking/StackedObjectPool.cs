using System.Collections.Generic;
using UnityEngine;

public interface IStackable
{
    public Point GetDockingPoint();
    public void StackSelf(Point point);
    public void StackToSelf(IStackable stackable);
    public void Unstack();
    public bool InStack();
}

public abstract class StackedObjectPool : MonoBehaviour
{
    private protected IStackable _firstElement;

    private protected List<IStackable> _stackables;

    public int GetStackablesCount() =>
        _stackables.Count;

    public void Add(IStackable stackable)
    {
        if (_stackables.Contains(stackable) == true)
        {
            Debug.LogWarning("Object has in pool already :: StackedObjectPool at Add()");
            return;
        }
        _stackables.Add(stackable);
    }

    public virtual void Awake()
    {
        if (_firstElement == null)
            Debug.LogError("StackedObjectPool dont have first element. It would work uncorrectly :: StackedObjectPool at Awake()");

        _stackables = new List<IStackable>();
        if (_firstElement != null)
            _stackables.Add(_firstElement);
    }
}

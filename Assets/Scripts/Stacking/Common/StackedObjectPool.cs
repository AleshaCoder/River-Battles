using System;
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
    [SerializeField] private protected bool _needFirstElement;
    private protected IStackable _firstElement;

    private protected List<IStackable> _stackables;

    public Action<int> OnStackableCountChanged;

    public int GetStackablesCount() =>
        _stackables.Count;

    public bool Add(IStackable stackable)
    {
        if (_stackables.Contains(stackable) == true)
        {
            Debug.LogWarning("Object has in pool already :: StackedObjectPool at Add()");
            return false;
        }
        _stackables.Add(stackable);
        OnStackableCountChanged?.Invoke(_stackables.Count);
        return true;
    }

    public bool Remove(IStackable stackable)
    {
        if (_stackables.Remove(stackable) == false)
        {
            Debug.LogWarning("Object has not in pool :: StackedObjectPool at Remove()");
            return false;
        }
        OnStackableCountChanged?.Invoke(_stackables.Count);
        return true;
    }

    public virtual void Awake()
    {
        if (_needFirstElement == true)
        {
            if (_firstElement == null)
                Debug.LogError("StackedObjectPool dont have first element. It would work uncorrectly :: StackedObjectPool at Awake()");
        }

        _stackables = new List<IStackable>();
        if (_firstElement != null)
            _stackables.Add(_firstElement);
    }
}

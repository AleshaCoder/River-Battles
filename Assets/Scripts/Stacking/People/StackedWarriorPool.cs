using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackedWarriorPool : StackedObjectPool
{
    public static StackedWarriorPool Instance { get; private set; }

    public int GetWarriorsCount()
    {
        return _stackables.Count;
    }

    public List<Warrior> GetWarriors()
    {
        List<Warrior> warriors = new List<Warrior>();
        foreach (var item in _stackables)
        {
            warriors.Add((Warrior)item);
        }
        return warriors;
    }

    public bool TryKill()
    {
        var warrior = _stackables[_stackables.Count - 1];
        if (Remove(warrior) == true)
        {
            ((Warrior)warrior).Kill();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Clear()
    {
        foreach (var item in _stackables)
        {
            ((Warrior)item).Kill(true);
        }
        _stackables.Clear();
        OnStackableCountChanged?.Invoke(_stackables.Count);
    }

    public override void Awake()
    {
        if (Instance == null)
            Instance = this;
        base.Awake();
    }
}

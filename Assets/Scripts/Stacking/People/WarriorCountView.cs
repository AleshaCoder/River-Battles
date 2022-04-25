using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WarriorCountView : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private TMP_Text _text;


    public void Show()
    {
        _object.SetActive(true);
    }

    public void Hide()
    {
        _object.SetActive(false);
    }

    private void Refresh(int count)
    {
        _text.text = count.ToString();
    }

    private void OnEnable()
    {
        Refresh(StackedWarriorPool.Instance.GetWarriorsCount());
        Debug.Log(StackedWarriorPool.Instance.name);
        StackedWarriorPool.Instance.OnStackableCountChanged += Refresh;
    }

    private void OnDisable()
    {
        StackedWarriorPool.Instance.OnStackableCountChanged -= Refresh;
    }
}

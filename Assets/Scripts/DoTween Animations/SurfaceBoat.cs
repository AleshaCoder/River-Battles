using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SurfaceBoat : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _endPosition;

    public void Animate()
    {
        Debug.Log("Animate");
        transform.localPosition = _startPosition;
        transform.DOLocalMove(_endPosition, _time).Play();
    }
}

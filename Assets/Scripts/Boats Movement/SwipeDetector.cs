
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private float _swipeSpeed = 0.025f;
    [SerializeField] private float _swipeMinValue = -1f;
    [SerializeField] private float _swipeMaxValue = 1f;

    public float XDragValue;
    public Action<float> OnSwipeEvent;

    public static SwipeDetector Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Swipe Detector Should Be 1 on scene! Your Code Can Work Uncorrectly");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {}

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x != 0)
            {
                XDragValue += eventData.delta.x * _swipeSpeed;
                XDragValue = Mathf.Clamp(XDragValue, _swipeMinValue, _swipeMaxValue);
            }
        }
    }
}
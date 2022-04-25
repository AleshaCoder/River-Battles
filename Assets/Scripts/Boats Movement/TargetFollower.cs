using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [Header("Common Settings")]
    [SerializeField]
    private Transform _targetTransform;

    [SerializeField]
    private float _distanceToTarget = 15.0f;

    [SerializeField]
    private float _followSpeed = 3.0f;

    [Space]
    [Header("Axis Settings For Following")]
    [SerializeField]
    private bool _followX;

    [SerializeField]
    private bool _followY;

    [SerializeField]
    private bool _followZ;

    [Space]
    [Header("Follow or No in Game")]
    [SerializeField] private bool _follow = false;

    public Transform TargetTransform
    {
        get
        {
            if (_targetTransform == null)
            {
                _follow = false;
                Debug.LogError("Target Transform Not Set In Inspector :: TargetFollower.cs at TargetTransform");
            }
            return _targetTransform;
        }
        set { _targetTransform = value; }
    }

    public float DistanceToTarget
    {
        get { return _distanceToTarget; }
        set { _distanceToTarget = Mathf.Max(0.0f, value); }
    }

    public float FollowSpeed
    {
        get { return _followSpeed; }
        set { _followSpeed = value; }
    }

    private Vector3 _relativePosition
    {
        get
        {
            Vector3 relativePosition = TargetTransform.position - transform.forward * DistanceToTarget;
            float x = (_followX == true) ? relativePosition.x : transform.position.x;
            float y = (_followY == true) ? relativePosition.y : transform.position.y;
            float z = (_followZ == true) ? relativePosition.z : transform.position.z;
            return new Vector3(x, y, z);
        }
    }

    public void Follow(Transform target)
    {
        _distanceToTarget = Vector3.Distance(TargetTransform.position, transform.position);
        transform.position = _relativePosition;
        _follow = true;
    }

    public void Follow(Transform target, bool x = true, bool y = true, bool z = true, float speed = 1, float distance = -1)
    {
        TargetTransform = target;

        _followX = x;
        _followY = y;
        _followZ = z;

        FollowSpeed = speed;

        if (distance == -1)
        {
            _distanceToTarget = Vector3.Distance(TargetTransform.position, transform.position);
        }
        else
            _distanceToTarget = distance;

        //transform.position = _relativePosition;
        _follow = true;
    }

    public void StopFollow()
    {
        _follow = false;
    }

    public void OnValidate()
    {
        DistanceToTarget = _distanceToTarget;
        FollowSpeed = _followSpeed;
    }

    public void Update()
    {
        if (_follow == false)
            return;
        var direction = _relativePosition - transform.position;
        transform.LookAt(TargetTransform);
        transform.Translate(direction * FollowSpeed * Time.deltaTime);
       // transform.position = Vector3.Lerp(transform.position, _relativePosition, FollowSpeed * Time.deltaTime);        
    }
}

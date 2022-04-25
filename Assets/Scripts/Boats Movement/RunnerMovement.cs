using UnityEngine;

public class RunnerMovement : MonoBehaviour, IService
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _forwardSpeed;
    [SerializeField] private float _minXValue, _maxXValue;

    [SerializeField] private SwipeDetector _swipeDetector;

    [SerializeField] private bool _canMoving;

    private Vector3 _direction = new Vector3(0, 0, 0);
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    private bool _moveForward;
    private float _standartForwardSpeed;

    public float ForwardSpeed => _forwardSpeed;
    public bool CanMoving => _canMoving;

    public void Init()
    {
        _moveForward = false;
        _canMoving = false;
        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _standartForwardSpeed = _forwardSpeed;
        GameStarter.OnGameStarted += StartMoving;
    }

    public void MoveToStartPosition()
    {
        transform.position = _startPosition;
        transform.rotation = _startRotation;
        _forwardSpeed = _standartForwardSpeed;
    }

    public void MoveForward()
    {
        _moveForward = true;
    }

    public void SetFinishSpeed()
    {
        _forwardSpeed = _standartForwardSpeed * 2;
    }
    public void SetLowSpeed()
    {
        _forwardSpeed = _standartForwardSpeed / 1.5f;
    }

    public void Stop()
    {
        _canMoving = false;
        _moveForward = false;
    }

    public void StartMoving()
    {
        _canMoving = true;
    }

    private void Update()
    {
        if (_canMoving == false)
            return;

        float newLocalXPosition = transform.localPosition.x;

        if (_moveForward == false)
            newLocalXPosition = Mathf.Lerp(newLocalXPosition, _swipeDetector.XDragValue * _maxXValue, Time.deltaTime * _horizontalSpeed);

        if ((newLocalXPosition > 0 && transform.localPosition.x > _maxXValue) ||
           (newLocalXPosition < 0) && transform.localPosition.x < _minXValue)
            newLocalXPosition = transform.localPosition.x;

        transform.localPosition = new Vector3(newLocalXPosition, transform.localPosition.y, transform.localPosition.z);
        _direction = new Vector3(0, 0, _forwardSpeed);
        transform.Translate(_direction * Time.deltaTime);
    }
}

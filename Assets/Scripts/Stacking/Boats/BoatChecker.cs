using System;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BoatChecker : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Collider _collider;

    [SerializeField] private bool _isCheacking;

    public Action<Boat> OnFindingBoat;

    public void SwitchOn()
    {
        _isCheacking = true;
    }

    public void SwitchOff()
    {
        _isCheacking = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCheacking == false)
            return;

        if (collision.gameObject.TryGetComponent(out Boat boat))
        {
            StackedBoatsPool.Instance.StackBoat(boat);
            SwitchOff();
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = false;
    }
}

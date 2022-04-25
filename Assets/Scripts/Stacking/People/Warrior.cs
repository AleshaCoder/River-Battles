using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IStackable
{
    public Gun Gun;
    [SerializeField] private protected Animator _killAnimation;
    [SerializeField] private protected Material _killMaterial;
    [SerializeField] private protected Point _dockingPoint;
    [SerializeField] private protected SkinnedMeshRenderer _skinnedMeshRenderer;
    public Point GetDockingPoint() => _dockingPoint;

    private protected Point _usingPoint;

    public bool InStack()
    {
        return false;
    }

    public void StackSelf(Point point, bool needRotation = true, bool needParent = false)
    {
        if (_usingPoint != null)
            _usingPoint.Free();
        _usingPoint = point;
        point.Take();
        StartCoroutine(AnimMoveToPoint(point, 15, 0.4f, 0.5f, needRotation, needParent));
    }

    public void StackSelf(Point point)
    {
        _usingPoint = point;
        point.Take();
        StartCoroutine(AnimMoveToPoint(point, 15, 0.4f, 0.5f));
    }

    public virtual IEnumerator AnimMoveToPoint(Point point, float height, float jumpTime, float rotationTime, bool needRotation = true, bool needParent = true)
    {
        var localPosDocking = _dockingPoint.transform.localPosition;
        var directionX = transform.position.x - _dockingPoint.Position.x;
        var directionZ = transform.position.z - _dockingPoint.Position.z;
        var directionY = transform.position.y - _dockingPoint.Position.y;
        var startTime = jumpTime;
        var startDockingPosition = _dockingPoint.Position;
        var deltaHeight = 0.0f;

        while (jumpTime > 0)
        {
            var newPosition = Vector3.Lerp(startDockingPosition, point.Position, (startTime - jumpTime) / startTime);
            _dockingPoint.transform.position = newPosition;

            if (jumpTime > startTime / 2.0f)
            {
                deltaHeight += height * Time.deltaTime;
            }
            else
            {
                deltaHeight -= height * Time.deltaTime;
            }

            transform.position = new Vector3(_dockingPoint.Position.x + directionX,
           _dockingPoint.Position.y + directionY + deltaHeight,
           _dockingPoint.Position.z + directionZ);

            _dockingPoint.transform.localPosition = localPosDocking;
            jumpTime -= Time.deltaTime;
            yield return null;
        }

        _dockingPoint.SetPosition(point);

        transform.position = new Vector3(_dockingPoint.Position.x + directionX,
            _dockingPoint.Position.y + directionY,
            _dockingPoint.Position.z + directionZ);

        _dockingPoint.transform.localPosition = localPosDocking;

        if (needParent == true)
            transform.parent = point.transform;

        if (needRotation == true)
        {
            startTime = rotationTime;

            while (rotationTime > 0)
            {
                transform.eulerAngles = (new Vector3(0, -90, 0) * (startTime - rotationTime));
                rotationTime -= Time.deltaTime;
                yield return null;
            }

            transform.eulerAngles = new Vector3(0, -90, 0);
        }

        yield break;
    }

    public virtual void Kill(bool fast = false)
    {
        if (_usingPoint != null)
            _usingPoint.Free();
        _skinnedMeshRenderer.sharedMaterial = _killMaterial;
        Debug.Log(gameObject.name + " Killed");
        _killAnimation.enabled = true;
        _killAnimation.Play("Death");
        if (fast == true)
            Destroy(gameObject);
        else
            Destroy(gameObject, 3);
    }

    public abstract void StackToSelf(IStackable stackable);

    public abstract void Unstack();

}

public class Warrior : Unit
{
    public override void StackToSelf(IStackable stackable)
    { }

    public override void Unstack()
    {
        _usingPoint.Free();
    }
}

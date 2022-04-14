using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Vector3 Rotation => transform.localEulerAngles;

    public void SetPosition(Point point)
    {
        transform.position = point.Position;
    }
}

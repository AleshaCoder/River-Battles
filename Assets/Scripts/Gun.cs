using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] private Point _startPoint;
    [SerializeField] private GameObject _bulletPrefab;
    private float _bulletSpeed = 100;


    public void Shot(Transform target)
    {
        var bullet = Instantiate(_bulletPrefab, _startPoint.Position, Quaternion.identity);
        Move(bullet, target);
    }

    private void Move(GameObject bullet, Transform target)
    {
        var time = Vector3.Distance(bullet.transform.position, target.position) / _bulletSpeed;
        bullet.transform.LookAt(target);
        var anim = bullet.transform.DOMove(target.position, time).Play();
        anim.onComplete += () => Destroy(bullet);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField] private Point _startPoint;
    [SerializeField] private GameObject _bulletPrefab;


    public void Shot(Transform target)
    {
        var bullet = Instantiate(_bulletPrefab, _startPoint.Position, Quaternion.identity);
        Move(bullet, target);
    }

    private void Move(GameObject bullet, Transform target)
    {
        bullet.transform.LookAt(target);
        var anim = bullet.transform.DOMove(target.position, 0.3f).Play();
        anim.onComplete += () => Destroy(bullet);
    }
}

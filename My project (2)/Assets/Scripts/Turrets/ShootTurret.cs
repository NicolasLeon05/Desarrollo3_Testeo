using System.Collections.Generic;
using UnityEngine;
public class ShootTurret : Turret, IBulletConfig
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletGO;
    [SerializeField] private int _maxBullets;
    [SerializeField] private List<GameObject> _bullets = new();
    [SerializeField] private Transform _bulletStartPosition;

    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public GameObject BulletGO { get => _bulletGO; set => _bulletGO = value; }
    public int MaxBullets { get => _maxBullets; set => _maxBullets = value; }
    public List<GameObject> Bullets { get => _bullets; set => _bullets = value; }
    public Transform BulletStartPos { get => _bulletStartPosition; set => _bulletStartPosition = value; }

    public void Fire1(Vector2 direction)
    {
        if (Bullets.Count < MaxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(BulletGO, BulletStartPos.position, Quaternion.identity);

            Bullet bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.direction = direction;
            bulletComponent.speed = BulletSpeed;

            Bullets.Add(newBullet);
        }
        else
        {
            for (int i = 0; i < Bullets.Count; i++)
                if (!Bullets[i].gameObject.activeSelf)
                {
                    Bullets[i].GetComponent<Bullet>().ResetBullet();
                    return;
                }
        }
    }

}

using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : Turret, IBulletConfig
{
    [SerializeField] private Vector2 _direction;

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

    protected override void Awake()
    {
        if (BulletGO == null)
            BulletGO = GameObject.Find("bullet");

        base.Awake();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= cooldown)
        {
            Fire();
            _timer = 0f;
        }
    }

    private void Fire()
    {
        if (Bullets.Count < MaxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(BulletGO, BulletStartPos.position, Quaternion.identity);

            Bullet bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.direction = _direction;
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

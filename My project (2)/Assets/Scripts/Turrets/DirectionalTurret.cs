using System.Collections.Generic;
using UnityEngine;

public class DirectionalTurret : Turret, IAreaTurret, IBulletConfig
{
    [SerializeField] private List<GameObject> _enemiesCollided;

    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject _bulletGO;
    [SerializeField] private int _maxBullets;
    [SerializeField] private List<GameObject> _bullets = new();
    [SerializeField] private Transform _bulletStartPos;

    [SerializeField] private GameObject _currentTarget;

    public List<GameObject> EnemiesCollided { get => _enemiesCollided; private set => _enemiesCollided = value; }
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    public GameObject BulletGO { get => _bulletGO; set => _bulletGO = value; }
    public int MaxBullets { get => _maxBullets; set => _maxBullets = value; }
    public List<GameObject> Bullets { get => _bullets; set => _bullets = value; }
    public Transform BulletStartPos { get => _bulletStartPos; set => _bulletStartPos = value; }

    protected override void Awake()
    {
        if (BulletGO == null)
            BulletGO = GameObject.Find("bullet");

        EnemiesCollided = new List<GameObject>();

        base.Awake();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        ClearEnemyList();

        if (_timer >= cooldown && EnemiesCollided.Count > 0)
        {
            Fire();
            _timer = 0f;
        }
    }

    public void CollisionEnter(Collision2D collision)
    {
        EnemiesCollided.Add(collision.gameObject);

        EnemiesCollided[0] = EnemiesCollided[0] != null ? EnemiesCollided[0] : collision.gameObject;
        //_currentTarget

    }

    public void CollisionExit(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyPathFinding3>())
            return;

        EnemiesCollided.Remove(collision.gameObject);
        //Debug.Log()
    }

    private void Fire()
    {
        if (Bullets.Count < MaxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(BulletGO, BulletStartPos.position, Quaternion.identity);

            Bullet2 bulletComponent = newBullet.GetComponent<Bullet2>();
            bulletComponent.speed = BulletSpeed;
            bulletComponent.target = EnemiesCollided[0] != null ? EnemiesCollided[0] : null;

            Bullets.Add(newBullet);
        }
        else
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (!Bullets[i].gameObject.activeSelf)
                {
                    Bullets[i].GetComponent<Bullet2>().ResetBullet();
                    Bullets[i].GetComponent<Bullet2>().target = EnemiesCollided[0];
                    return;
                }
            }
        }
    }


    private void ClearEnemyList()
    {
        for (int i = 0; i < EnemiesCollided.Count; i++)
        {
            if (!EnemiesCollided[i].gameObject.activeSelf)
            {
                EnemiesCollided.Remove(EnemiesCollided[i]);
                return;
            }
        }
    }
}

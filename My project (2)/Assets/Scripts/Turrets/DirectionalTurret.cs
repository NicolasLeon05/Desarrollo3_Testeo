using System.Collections.Generic;
using UnityEngine;

public class DirectionalTurret : ShootTurret, IAreaTurret
{
    [SerializeField] private List<GameObject> _enemiesCollided;

    [SerializeField] private GameObject _currentTarget;

    public List<GameObject> EnemiesCollided { get => _enemiesCollided; private set => _enemiesCollided = value; }

    protected override void Awake()
    {
        if (BulletGO == null)
            Debug.LogError("BulletGO not found");

        EnemiesCollided = new List<GameObject>();

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        ClearEnemyList();

        if (_timer >= cooldown && EnemiesCollided.Count > 0)
        {
            Fire();
            _timer = 0f;
        }
    }

    public void CollisionEnter(Collision2D collision)
    {
        EnemiesCollided?.Add(collision.gameObject);

        EnemiesCollided[0] = EnemiesCollided[0] != null ? EnemiesCollided[0] : collision.gameObject;
        //_currentTarget

    }

    public void CollisionExit(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyPathFinding3>())
            return;

        EnemiesCollided?.Remove(collision.gameObject);
        //Debug.Log()
    }

    public void Fire()
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
                if (!Bullets[i].gameObject.activeSelf)
                {
                    Bullets[i].GetComponent<Bullet2>().ResetBullet();
                    Bullets[i].GetComponent<Bullet2>().target = EnemiesCollided[0];
                    return;
                }
        }
    }


    private void ClearEnemyList()
    {
        for (int i = 0; i < EnemiesCollided.Count; i++)
            if (!EnemiesCollided[i].gameObject.activeSelf)
            {
                EnemiesCollided.Remove(EnemiesCollided[i]);
                return;
            }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DirectionalTurret : Turret, IAreaTurret
{
    [SerializeField] private float _bulletSpeed;
    private float _timer;

    [SerializeField] private GameObject _bulletGameObject;

    [SerializeField] private int _maxBullets;
    private List<GameObject> _bullets = new List<GameObject>();

    [SerializeField] private Transform _bulletStartPosition;

    [SerializeField] private List<GameObject> _enemiesCollided;
    [SerializeField] private GameObject _currentTarget;

    protected override void Awake()
    {
        if (_bulletGameObject == null)
            _bulletGameObject = GameObject.Find("bullet");

        _enemiesCollided = new List<GameObject>();

        base.Awake();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        ClearEnemyList();

        if (_timer >= cooldown && _enemiesCollided.Count > 0)
        {
            Fire();
            _timer = 0f;
        }
    }

    public void CollisionEnter(Collision2D collision)
    {
        _enemiesCollided.Add(collision.gameObject);

        _enemiesCollided[0] = _enemiesCollided[0] != null ? _enemiesCollided[0] : collision.gameObject;
        //_currentTarget

    }

    public void CollisionExit(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyPathFinding3>())
            return;

        _enemiesCollided.Remove(collision.gameObject);
        //Debug.Log()
    }

    private void Fire()
    {
        if (_bullets.Count < _maxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(_bulletGameObject, _bulletStartPosition.position, Quaternion.identity);

            Bullet2 bulletComponent = newBullet.GetComponent<Bullet2>();
            bulletComponent.speed = _bulletSpeed;
            bulletComponent.target = _enemiesCollided[0] != null ? _enemiesCollided[0] : null;

            _bullets.Add(newBullet);
        }
        else
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (!_bullets[i].gameObject.activeSelf)
                {
                    _bullets[i].GetComponent<Bullet2>().ResetBullet();
                    _bullets[i].GetComponent<Bullet2>().target = _enemiesCollided[0];
                    return;
                }
            }
        }
    }


    private void ClearEnemyList()
    {
        for (int i = 0; i < _enemiesCollided.Count; i++)
        {
            if (!_enemiesCollided[i].gameObject.activeSelf)
            {
                _enemiesCollided.Remove(_enemiesCollided[i]);
                return;
            }
        }
    }
}

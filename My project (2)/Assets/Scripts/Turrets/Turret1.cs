using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret1 : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private float _bulletSpeed;
    private Vector2 _direction;
    private float _timer;

    [SerializeField] private GameObject _bulletGameObject;

    [SerializeField] private int _maxBullets;
    [SerializeField] private List<GameObject> _bullets = new List<GameObject>();

    [SerializeField] private Transform _bulletStartPosition;

    private List<GameObject> _enemiesCollided;

    private void Awake()
    {
        if (_bulletGameObject == null)
            _bulletGameObject = GameObject.Find("bullet");

        _enemiesCollided = new List<GameObject>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    public void CollisionEnter(Collision2D collision)
    {
        _enemiesCollided.Add(collision.gameObject);

        _enemiesCollided[0] = _enemiesCollided[0] ? _enemiesCollided[0] : collision.gameObject;

        if (_timer >= _cooldown)
        {
            Fire();
            _timer = 0f;
        }
    }

    public void CollisionExit(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyPathFinding3>())
            return;

        _enemiesCollided.Remove(collision.gameObject);
    }

    private void Fire()
    {
        if (_bullets.Count < _maxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(_bulletGameObject, _bulletStartPosition.position, Quaternion.identity);

            Bullet2 bulletComponent = newBullet.GetComponent<Bullet2>();
            bulletComponent.speed = _bulletSpeed;
            bulletComponent.target = _enemiesCollided[0] ? _enemiesCollided[0] : null;

            _bullets.Add(newBullet);
        }
        else
        {
            for (int i = 0; i < _bullets.Count; i++)
                if (!_bullets[i].gameObject.activeSelf)
                {
                    _bullets[i].GetComponent<Bullet2>().ResetBullet();
                    return;
                }
        }
    }

    private void CalculateDir(Vector3 target)
    {
        if (target != Vector3.zero)
            _direction = (target - transform.position).normalized;
        else
            _direction = Vector2.down;
    }
}

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int price;

    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _bulletSpeed;
    private float _timer;

    [SerializeField] private GameObject _bulletGameObject;

    [SerializeField] private int _maxBullets;
    [SerializeField] private List<GameObject> _bullets = new List<GameObject>();

    [SerializeField] private Transform _bulletStartPosition;

    private void Awake()
    {
        if (_bulletGameObject == null)
            _bulletGameObject = GameObject.Find("bullet");
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _cooldown)
        {
            Fire();
            _timer = 0f;
        }
    }

    private void Fire()
    {
        if (_bullets.Count < _maxBullets)
        {
            //Modificar para que los datos de la bala se pasen desde aca
            GameObject newBullet = Instantiate(_bulletGameObject, _bulletStartPosition.position, Quaternion.identity);

            Bullet bulletComponent = newBullet.GetComponent<Bullet>();
            bulletComponent.direction = _direction;
            bulletComponent.speed = _bulletSpeed;

            _bullets.Add(newBullet);
        }
        else
        {
            for (int i = 0; i < _bullets.Count; i++)
                if (!_bullets[i].gameObject.activeSelf)
                {
                    _bullets[i].GetComponent<Bullet>().ResetBullet();
                    return;
                }
        }
    }


}

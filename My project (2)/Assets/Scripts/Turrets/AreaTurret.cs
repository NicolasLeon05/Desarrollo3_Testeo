using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AreaTurret : Turret, IAreaTurret
{
    [SerializeField] private float _damage = 30;
    private float _timer;

    [SerializeField] private List<GameObject> _enemiesCollided;

    protected override void Awake()
    {
        _enemiesCollided = new List<GameObject>();

        base.Awake();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        ClearEnemyList();

        if (_timer >= cooldown && _enemiesCollided.Count > 0)
        {
            _timer = 0f;

            for (int i = 0; i < _enemiesCollided.Count; i++)
            {
                GameObject enemyGO = _enemiesCollided[i];
                var enemy = enemyGO?.GetComponent<EnemyPathFinding3>();
                
                enemy?.TakeDamage(_damage);
            }
        }
    }

    public void CollisionEnter(Collision2D collision)
    {
        _enemiesCollided?.Add(collision.gameObject);
    }

    public void CollisionExit(Collision2D collision)
    {
        if (!collision.gameObject.GetComponent<EnemyPathFinding3>())
            return;

        _enemiesCollided?.Remove(collision.gameObject);
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

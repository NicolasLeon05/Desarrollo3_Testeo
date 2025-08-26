using Unity.Mathematics;
using UnityEngine;

public class EnemyPathFinding3 : MonoBehaviour
{
    //[SerializeField] EnemyStatsMultiplier _enemyStatsMultiplier;
    [SerializeField] private TargetManager _TargetManager;
    [SerializeField] private float _speed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    private GameObject _currentTarget;
    private int _currentTargetIndex;

    [SerializeField] private SliderUpdater _healthBar;

    void Start()
    {
        _currentTargetIndex = 0;
        _currentTarget = _TargetManager.Targets[_currentTargetIndex];

        if (_healthBar == null)
            _healthBar = GetComponentInChildren<SliderUpdater>();

        _currentHealth = _maxHealth;
    }

    void Update()
    {
        if (_currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget.transform.position, _speed * /*_enemyStatsMultiplier.speedMultiplier * */ Time.deltaTime);
            CheckTargetReached();
        }
    }

    private void CheckTargetReached()
    {
        float distance = Vector2.Distance(transform.position, _currentTarget.transform.position);

        if (distance < math.EPSILON)
        {
            if (_currentTargetIndex < _TargetManager.Targets.Count - 1)
            {
                _currentTargetIndex++;
                _currentTarget = _TargetManager.Targets[_currentTargetIndex];
            }
            else if (_currentTargetIndex == _TargetManager.Targets.Count - 1)
                _currentTarget = _TargetManager.FinalTarget;
            else
                _currentTarget = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
            {
                float damageToTake = bullet.damage;
                TakeDamage(damageToTake);
                Debug.Log(damageToTake + " damage taken!");
                return;
            }

            if (collision.gameObject.TryGetComponent<Bullet2>(out Bullet2 bullet2))
            {
                float damageToTake = bullet2.damage;
                TakeDamage(damageToTake);
                Debug.Log(damageToTake + " damage taken!");
                return;
            }

        }
        Debug.Log("This enemy collided with another object");
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth < 0)
            this.gameObject.SetActive(false);

        _healthBar.UpdateSlider(_currentHealth, _maxHealth);
    }
}
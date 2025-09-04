using UnityEngine;

public class BasicTurret : ShootTurret
{
    [SerializeField] private Vector2 _direction;

    protected override void Awake()
    {
        if (BulletGO == null)
            Debug.LogError("BulletGO not found");

        base.Awake();
    }

    protected override void Update()
    {
        base.Update();

        if (_timer >= cooldown)
        {
            Fire1(_direction);
            _timer = 0f;
        }
    }

}

using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float _timer;
    public int price;

    protected virtual void Awake()
    {
        EventTriggerer.Trigger<ITurretSpawnEvent>(new TurretSpawnEvent(this.gameObject));
    }
}

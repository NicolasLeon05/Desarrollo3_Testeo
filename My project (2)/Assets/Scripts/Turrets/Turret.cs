using UnityEngine;

public class Turret : MonoBehaviour
{
    public int price;
    [SerializeField] protected float cooldown;

    protected virtual void Awake()
    {
        EventTriggerer.Trigger<ITurretSpawnEvent>(new TurretSpawnEvent(this.gameObject));
    }
}

using UnityEngine;

public class AreaNotifier2 : MonoBehaviour
{
    private Turret2 _parentTurret;

    private void Awake()
    {
        _parentTurret = gameObject.GetComponentInParent<Turret2>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _parentTurret.CollisionEnter(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _parentTurret.CollisionExit(collision);

    }
}

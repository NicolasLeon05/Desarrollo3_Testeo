using UnityEngine;

public class AreaNotifier : MonoBehaviour
{
    private Turret1 _parentTurret;

    private void Awake()
    {
        _parentTurret = gameObject.GetComponentInParent<Turret1>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _parentTurret.CollisionEnter(collision);

        Debug.Log("collision E N T E R - " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _parentTurret.CollisionExit(collision);

        Debug.Log("collision E X I T - " + collision.gameObject.name);
    }
}

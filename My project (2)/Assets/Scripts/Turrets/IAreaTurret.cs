using UnityEngine;

public interface IAreaTurret
{
    void CollisionEnter(Collision2D collision);
    void CollisionExit(Collision2D collision);
}

using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class Bullet : MonoBehaviour
{

    [Range(0.1f, 2f)] public float speed;
    public float range;
    public float damage;

    public Vector2 direction;
    public Vector2 initialPosition;

    private void OnEnable()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        Move();
        CheckLimitReached();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, speed * Time.deltaTime);
    }

    private void CheckLimitReached()
    {
        float distanceTraveled = Vector2.Distance(transform.position, initialPosition);
        if (distanceTraveled >= range)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ResetBullet()
    {
        //Debug.Log("Bullet reseted");
        transform.position = initialPosition;
        this.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = initialPosition;

        if (collision.gameObject.CompareTag("bullet"))
            return;

        this.gameObject.SetActive(false);
    }
}

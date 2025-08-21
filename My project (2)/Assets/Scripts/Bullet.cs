using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class Bullet : MonoBehaviour
{

<<<<<<< Updated upstream:My project (2)/Assets/Scripts/Bullet.cs
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public Vector2 direction;
=======
    [Range(0.1f, 2f)] public float speed;
    public float range;
    public float damage;
    public Vector2 direction;
>>>>>>> Stashed changes:My project (2)/Assets/Scripts/Enemy/Bullet.cs
    public Vector2 _initialPosition;

    private void OnEnable()
    {
        _initialPosition = transform.position;
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
        float distanceTraveled = Vector2.Distance(transform.position, _initialPosition);
        if (distanceTraveled >= range)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void ResetBullet()
    {
        //Debug.Log("Bullet reseted");
        transform.position = _initialPosition;
        this.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
    }
}

using UnityEngine;

public class Bullet2 : MonoBehaviour
{

    [Range(0.1f, 2f)] public float speed;
    public float range;
    public float damage;

    public GameObject target;
    public Vector2 direction;
    public Vector2 initialPosition;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        if(!target)
            Debug.LogError("No target assigned to bullet");

        var currentTarget = target == null ? (Vector2)transform.position + direction : (Vector2)target.transform.position;

        transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        
        if ((Vector2)transform.position == currentTarget)
            gameObject.SetActive(false);
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
        if (collision.gameObject.CompareTag("bullet"))
            return;

        this.gameObject.SetActive(false);
    }
}

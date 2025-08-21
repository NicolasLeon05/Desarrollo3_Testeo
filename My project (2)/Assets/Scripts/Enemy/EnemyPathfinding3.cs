using Unity.Mathematics;
using UnityEngine;

public class EnemyPathFinding3 : MonoBehaviour
{
    [SerializeField] TargetManager TargetManager;
    [SerializeField] private float speed;
    private GameObject currentTarget;
    private int currentTargetIndex;

    void Start()
    {
        currentTargetIndex = 0;
        currentTarget = TargetManager.Targets[currentTargetIndex];
    }

    void Update()
    {
        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.fixedDeltaTime);
            CheckTargetReached();
        }
    }

    private void CheckTargetReached()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

        Debug.Log("Distance: " + distance + " | Epsilon: " + math.EPSILON);
        if (distance < math.EPSILON)
        {
            Debug.Log("Option 1 entered");
            if (currentTargetIndex < TargetManager.Targets.Count - 1)
            {
                currentTargetIndex++;
                currentTarget = TargetManager.Targets[currentTargetIndex];
            }
            else if (currentTargetIndex == TargetManager.Targets.Count - 1)
                currentTarget = TargetManager.FinalTarget;
            else
                currentTarget = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("bullet"))
            return;

        Debug.Log("This enemy collided with another object");
        Destroy(this.gameObject);
    }
}

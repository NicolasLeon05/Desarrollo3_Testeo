using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyPathFinding2 : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    [SerializeField] private float speed;
    private GameObject currentTarget;
    private int currentTargetIndex;

    private void Start()
    {
        currentTargetIndex = 0;
        currentTarget = targets[currentTargetIndex];
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.transform.position, speed * Time.deltaTime);
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
            if(currentTargetIndex < targets.Count - 1)
            {
                currentTargetIndex++;
                currentTarget = targets[currentTargetIndex];
            }
            else
            {
                currentTarget = null;
            }
        }
        //if (Mathf.Approximately(distance, 0))
        //{
        //    Debug.Log("Option 2 entered");
        //    if (currentTargetIndex < targets.Count - 1)
        //    {
        //        currentTargetIndex++;
        //        currentTarget = targets[currentTargetIndex];
        //    }
        //    else
        //    {
        //        currentTarget = null;
        //    }
        //}
    }
}

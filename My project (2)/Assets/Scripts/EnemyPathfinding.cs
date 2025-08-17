using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.AI;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private List<Transform> targets = new List<Transform>();
    private Transform currentTarget;
    private NavMeshAgent agent;
    private int currentTargetIndex;

    void Start()
    {
        currentTargetIndex = 0;
        currentTarget = targets[currentTargetIndex];
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.SetDestination(currentTarget.position);
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            CheckTargetReached();
        }
    }

    private void CheckTargetReached()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);

        //Debug.Log("Distance: " + distance + " | Epsilon: " + math.EPSILON);
        //if (distance < math.EPSILON)
        //{
        //    Debug.Log("Option 1 entered");
        //    if (currentTargetIndex < targets.Count - 1)
        //    {
        //        currentTargetIndex++;
        //        currentTarget = targets[currentTargetIndex];
        //        agent.SetDestination(currentTarget.position);
        //    }
        //    else
        //    {
        //        currentTarget = null;
        //    }
        //}
        if (Mathf.Approximately(distance, 0))
        {
            Debug.Log("Option 2 entered");
            if (currentTargetIndex < targets.Count - 1)
            {
                currentTargetIndex++;
                currentTarget = targets[currentTargetIndex];
                agent.SetDestination(currentTarget.position);
            }
            else
            {
                currentTarget = null;
            }
        }
    }

}

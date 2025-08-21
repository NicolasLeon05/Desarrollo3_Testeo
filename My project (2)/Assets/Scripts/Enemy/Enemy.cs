using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private float _speed;

    [SerializeField] private List<GameObject> _targets = new List<GameObject>();
    private GameObject _currentTarget;
    private int _currentTargetIndex;

    private void Start()
    {
        _currentTargetIndex = 0;
        _currentTarget = _targets[_currentTargetIndex];
    }

    private void Update()
    {
        if (_currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget.transform.position, _speed * Time.fixedDeltaTime);
            CheckTargetReached();
        }
    }

    private void CheckTargetReached()
    {
        float distance = Vector2.Distance(transform.position, _currentTarget.transform.position);

        //Debug.Log("Distance: " + distance + " | Epsilon: " + math.EPSILON);
        if (distance < math.EPSILON)
        {
            //Debug.Log("Option 1 entered");
            if (_currentTargetIndex < _targets.Count - 1)
            {
                _currentTargetIndex++;
                _currentTarget = _targets[_currentTargetIndex];
            }
            else
            {
                _currentTarget = null;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("This enemy collided with another object");
        Destroy(this.gameObject);
    }

}

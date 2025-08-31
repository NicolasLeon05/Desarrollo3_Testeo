using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] private float _cooldown;
    private int _currentEnemyIndex;
    private float _timer;

    private void Start()
    {
        EventTriggerer.Trigger<IWaveCreateEvent>(new WaveCreateEvent(_enemies, gameObject));

        for (int i = 0; i < _enemies.Count; i++)
            _enemies[i].SetActive(false);

        _timer = 0;
        _currentEnemyIndex = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _cooldown && _currentEnemyIndex < _enemies.Count)
        {
            _enemies[_currentEnemyIndex].SetActive(true);
            _timer = 0;
            _currentEnemyIndex++;
        }
    }
}

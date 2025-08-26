using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSpawner : MonoBehaviour
{
    private List<GameObject> _turretPrefabs;

    [SerializeField] private InputActionReference _click;
    private SpriteRenderer _renderer;
    private GameObject _spawnedTurret;
    private int _nextTurretId = 0;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _click.action.canceled += OnClick;

        _spawnedTurret = null;
    }

    private void OnDestroy()
    {
        _click.action.canceled -= OnClick;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 mouseToScreenPos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseToScreenPos.z = 0;

        Bounds bounds = _renderer.sprite.bounds;
        float distance = Vector2.Distance(mouseToScreenPos, (Vector2)transform.position);

        if (distance < bounds.size.x/2)
            SpawnTurret();
    }

    private void SpawnTurret()
    {
        GameObject toDestroy = null;

        if (_spawnedTurret != null)
        {
            toDestroy = _spawnedTurret;
            _spawnedTurret = null;

            Destroy(toDestroy);

            if (_nextTurretId >= _turretPrefabs.Count - 1)
                _nextTurretId = 0;
            else
                _nextTurretId++;
        }
        _spawnedTurret = Instantiate(_turretPrefabs[_nextTurretId]);
        _spawnedTurret.transform.position = transform.position;
    }

    public void SetTurretPrefabs(List<GameObject> turretPrefabs)
    {
        if (_turretPrefabs == null)
            _turretPrefabs = new List<GameObject>();

        foreach (var prefab in turretPrefabs)
        {
            if (prefab.GetComponent<Turret>() == null)
                throw new Exception("Prefab does not have Turret component attached");

            if (_turretPrefabs.Find(x => x.name == prefab.name) != null)
                throw new Exception("Duplicate turret prefab names are not allowed");

            _turretPrefabs.Add(prefab);
        }
    }
}
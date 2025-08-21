using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _turretPrefab;

    [SerializeField] private InputActionReference _click;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();

        _click.action.canceled += OnClick;
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

        //if ((mousePos.x > bounds.min.x &&
        //    mousePos.y > bounds.min.y &&
        //    mousePos.x < bounds.max.x &&
        //    mousePos.y < bounds.max.y))
        {
            SpawnTurret();
        }
    }

    private void SpawnTurret()
    {
        GameObject turret = Instantiate(_turretPrefab);
        turret.transform.position = transform.position;
    }
}
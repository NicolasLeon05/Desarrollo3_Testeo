using NavMeshPlus.Components;
using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class PathPiece : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    [SerializeField] private PathSettings _pathInfo;
    [SerializeField] public Direction SpawnDirection = Direction.Right;
    [SerializeField] private TileSet _tileSet;
    private Direction _previousDir = Direction.Right;
    private SpriteRenderer _spriteRenderer;

    private GameObject _pathPrefab;

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (_pathInfo == null)
        {
            Debug.LogError("PathInfo is not assigned!");
            return;
        }

        _pathPrefab = _pathInfo.PathPrefab;

    }

    private Direction GetOpposite(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            Direction.Right => Direction.Left,
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };
    }

    private bool AreOpposite(Direction dirA, Direction dirB)
    {
        return GetOpposite(dirA) == dirB || GetOpposite(dirB) == dirA;
    }

    public void GeneratePath()
    {
        if (!_spriteRenderer)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        else if (!_spriteRenderer)
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        _spriteRenderer.sprite = _tileSet.GetTile(_previousDir);

        if (_pathInfo == null)
            Debug.LogError("PathInfo is not assigned!");

        if (_pathPrefab == null)
            _pathPrefab = _pathInfo.PathPrefab;

        if (_pathPrefab == null)
            Debug.LogError("PathPrefab is not assigned in PathInfo!");

        if (_previousDir != SpawnDirection && !AreOpposite(_previousDir, SpawnDirection))
        {
            EventTriggerer.Trigger<IPathDirectionChangeEvent>(new PathDirectionChangeEvent(this.gameObject));
            _spriteRenderer.sprite = _tileSet.GetTile(GetOpposite(_previousDir), SpawnDirection);
        }

        Vector2 spawnPos = CalculateSpawnPos();

        GameObject newPathPiece = Instantiate(_pathPrefab, spawnPos, Quaternion.identity, transform.parent);

        Selection.activeGameObject = newPathPiece;

        PathPiece pathPieceComponent = newPathPiece.GetComponent<PathPiece>();

        pathPieceComponent.SpawnDirection = SpawnDirection;

        pathPieceComponent._previousDir = SpawnDirection;

        Debug.Log("Previous Direction: " + _previousDir + ", New Direction: " + SpawnDirection);
    }

    private Vector2 CalculateSpawnPos()
    {
        if (_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not found!");
            return Vector2.zero;
        }

        Vector2 size = _spriteRenderer.sprite.bounds.size;


        Vector2 offSet = Vector2.zero;

        size.x -= 0.1f;
        size.y -= 0.1f;

        switch (SpawnDirection)
        {
            case Direction.Up:
                offSet = new(0, size.y);
                break;
            case Direction.Down:
                offSet = new(0, -size.y);
                break;
            case Direction.Left:
                offSet = new(-size.x, 0);
                break;
            case Direction.Right:
                offSet = new(size.x, 0);
                break;
            default:
                break;
        }

        return (Vector2)transform.position + offSet;
    }
}
#endif
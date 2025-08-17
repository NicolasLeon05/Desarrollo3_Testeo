using UnityEditor;
using UnityEngine;

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

    [SerializeField] private PathSettings pathInfo;
    [SerializeField] public Direction SpawnDirection = Direction.Right;
    private SpriteRenderer spriteRenderer;

    private GameObject pathPrefab;

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (pathInfo == null)
        {
            Debug.LogError("PathInfo is not assigned!");
            return;
        }

        pathPrefab = pathInfo.PathPrefab;

    }

    public void GeneratePath()
    {
        if (pathInfo == null)
            Debug.LogError("PathInfo is not assigned!");

        if (pathPrefab == null)
            pathPrefab = pathInfo.PathPrefab;

        if (pathPrefab == null)
            Debug.LogError("PathPrefab is not assigned in PathInfo!");

        Vector2 spawnPos = CalculateSpawnPos();

        GameObject pathPiece = Instantiate(pathPrefab, spawnPos, Quaternion.identity, transform.parent);

        Selection.activeGameObject = pathPiece;

        PathPiece pathPieceComponent = pathPiece.GetComponent<PathPiece>();

        pathPieceComponent.SpawnDirection = SpawnDirection;
    }

    private Vector2 CalculateSpawnPos()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not found!");
            return Vector2.zero;
        }

        Vector2 size = spriteRenderer.sprite.bounds.size;

        Vector2 offSet = Vector2.zero;

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

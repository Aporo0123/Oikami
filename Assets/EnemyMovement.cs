using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float moveTime = 0.2f;
    private bool isMoving = false;

    public Transform player;

    void Start()
    {
        // プレイヤーを自動検出（タグが"Player"である必要あり）
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;
    }

    public void MoveAwayFromPlayer()
    {
        if (isMoving) return;

        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector3 bestPosition = transform.position;
        float maxDistance = Vector3.Distance(transform.position, player.position);

        foreach (Vector2 dir in directions)
        {
            Vector3 nextPos = transform.position + (Vector3)dir;
            if (!IsInsideField(nextPos) || ObstacleManager.IsObstacle(nextPos))
                continue;

            float distance = Vector3.Distance(nextPos, player.position);
            if (distance > maxDistance)
            {
                maxDistance = distance;
                bestPosition = nextPos;
            }
        }

        if (bestPosition != transform.position)
        {
            StartCoroutine(MoveSmoothly(bestPosition));
        }
    }

    IEnumerator MoveSmoothly(Vector3 target)
    {
        isMoving = true;
        Vector3 start = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(start, target, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }

    bool IsInsideField(Vector3 pos)
    {
        return pos.x >= 0 && pos.x < FieldManagerStatic.Width &&
               pos.y >= 0 && pos.y < FieldManagerStatic.Height;
    }
}
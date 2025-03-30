using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveTime = 0.2f;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving) return;

        Vector2 move = Vector2.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            move = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            move = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            move = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            move = Vector2.right;

        if (move != Vector2.zero)
        {
            Vector3 targetPos = transform.position + (Vector3)move;

            if (IsInsideField(targetPos) && !ObstacleManager.IsObstacle(targetPos))
            {
                StartCoroutine(MoveSmoothly(targetPos));
            }
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

        // ターン制：敵をすべて動かす
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (var enemy in enemies)
        {
            enemy.MoveAwayFromPlayer();
        }
    }

    bool IsInsideField(Vector3 pos)
    {
        return pos.x >= 0 && pos.x < FieldManagerStatic.Width &&
               pos.y >= 0 && pos.y < FieldManagerStatic.Height;
    }
}
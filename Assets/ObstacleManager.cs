using UnityEngine;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    private static HashSet<Vector2> obstaclePositions = new HashSet<Vector2>();

    void Start()
    {
        // 障害物オブジェクト（タグ "Obstacle"）の位置を記録
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in obstacles)
        {
            Vector2 pos = new Vector2(Mathf.Round(obs.transform.position.x), Mathf.Round(obs.transform.position.y));
            obstaclePositions.Add(pos);
        }
    }

    public static bool IsObstacle(Vector3 pos)
    {
        Vector2 gridPos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
        return obstaclePositions.Contains(gridPos);
    }
}

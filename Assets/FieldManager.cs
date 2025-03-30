using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject tilePrefab;

    void Start()
    {
        // フィールドサイズを共有クラスに保存
        FieldManagerStatic.Width = width;
        FieldManagerStatic.Height = height;

        GenerateGrid();

        // カメラをフィールドの中心に移動
        float centerX = (width - 1) / 2f;
        float centerY = (height - 1) / 2f;
        Camera.main.transform.position = new Vector3(centerX, centerY, -10);
    }

    // マス目を生成する
    void GenerateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
    }
}
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // 移動速度
    private Rigidbody2D rb;       // Rigidbody2Dコンポーネントへの参照

    void Start()
    {
        // Rigidbody2Dを取得
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ユーザーの入力に応じて、横方向（X）と縦方向（Y）の速度を設定
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/Dキー または 左右矢印キー
        float moveVertical = Input.GetAxis("Vertical");     // W/Sキー または 上下矢印キー

        // 移動ベクトルを作成
        Vector2 moveDirection = new Vector2(moveHorizontal, moveVertical);

        // Rigidbody2Dの速度を設定
        rb.linearVelocity = moveDirection * moveSpeed;

        // キャラクターが画面から出ないように制限
        RestrictToScreenBounds();
    }

    void RestrictToScreenBounds()
    {
        // 画面の左端、右端、上端、下端のワールド座標を取得
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // キャラクターのサイズ（半径）を取得
        float characterWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float characterHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        // 画面端からはみ出さないようにキャラクターの位置を制限
        float clampedX = Mathf.Clamp(transform.position.x, screenBottomLeft.x + characterWidth, screenTopRight.x - characterWidth);
        float clampedY = Mathf.Clamp(transform.position.y, screenBottomLeft.y + characterHeight, screenTopRight.y - characterHeight);

        // 位置を制限された座標に設定
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

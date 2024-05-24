using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public bool isMoving;
    public float swayAmount = 0.1f; // 摇摆的幅度
    public float swaySpeed = 5f;    // 摇摆的速度
    public Transform spriteTransform; // 用于引用玩家的SpriteTransform

    private Vector3 originalSpritePosition;

    private void Start()
    {
        if (spriteTransform == null)
        {
            spriteTransform = transform;
        }

        originalSpritePosition = spriteTransform.localPosition; // 记录初始位置
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, moveY, 0);
        transform.Translate(move * moveSpeed * Time.deltaTime);

        isMoving = move.magnitude > 0;

        if (isMoving)
        {
            // 添加左右摇摆效果
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            spriteTransform.localPosition = originalSpritePosition + new Vector3(sway, 0, 0);
        }
        else
        {
            // 如果玩家没有移动，则恢复原始位置
            spriteTransform.localPosition = originalSpritePosition;
        }
    }
}

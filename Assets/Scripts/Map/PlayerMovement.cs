using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public bool isMoving;
    public float swayAmount = 0.1f; // 摇摆的幅度
    public float swaySpeed = 5f;    // 摇摆的速度
    public Transform spriteTransform; // 用于引用玩家的SpriteTransform


    private Vector3 originalSpritePosition;
    public float minX = -200f; // 移动边界的最小X位置
    public float maxX = 200f; // 移动边界的最大X位置
    public float minY = -200f; // 移动边界的最小Y位置
    public float maxY = 200f; // 移动边界的最大Y位置
    private Vector3 targetPosition; // 目标位置
    private float elapsedTime; // 已经经过的时间

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
        if (Input.GetMouseButtonDown(0))
        {
            // 鼠标左键被按下
            // 获取鼠标点击位置，转换为世界坐标
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // 确保 z 轴为 0（2D 游戏）

            // 开始移动
            isMoving = true;
            elapsedTime = 0;
        }

        // 移动角色
        if (isMoving)
        {
            // 计算移动方向和距离
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, targetPosition);

            // 计算插值位置
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, elapsedTime / (distance / moveSpeed));
            newPosition.z = 0; // 确保 z 轴为 0（2D 游戏）

            // 移动角色
            transform.position = newPosition;

            // 添加左右摇摆效果
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            spriteTransform.localPosition = originalSpritePosition + new Vector3(sway, 0, 0);

            // 更新已经经过的时间
            elapsedTime += Time.deltaTime;

            // 到达目标位置，停止移动
            if (elapsedTime >= (distance / moveSpeed))
            {
                isMoving = false;
                transform.position = targetPosition;
                spriteTransform.localPosition = originalSpritePosition;
            }

            // 边界检查
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
            transform.position = currentPosition;
        }

    }
}


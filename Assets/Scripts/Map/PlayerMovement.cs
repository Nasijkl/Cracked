using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public bool isMoving;
    public float swayAmount = 0.1f; // ҡ�ڵķ���
    public float swaySpeed = 5f;    // ҡ�ڵ��ٶ�
    public Transform spriteTransform; // ����������ҵ�SpriteTransform


    private Vector3 originalSpritePosition;
    public float minX = -200f; // �ƶ��߽����СXλ��
    public float maxX = 200f; // �ƶ��߽�����Xλ��
    public float minY = -200f; // �ƶ��߽����СYλ��
    public float maxY = 200f; // �ƶ��߽�����Yλ��
    private Vector3 targetPosition; // Ŀ��λ��
    private float elapsedTime; // �Ѿ�������ʱ��

    private Vector3 originalSpritePosition;


    private void Start()
    {
        if (spriteTransform == null)
        {
            spriteTransform = transform;
        }

        originalSpritePosition = spriteTransform.localPosition; // ��¼��ʼλ��
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
            // �������ҡ��Ч��
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            spriteTransform.localPosition = originalSpritePosition + new Vector3(sway, 0, 0);
        }
        else
        {
            // ������û���ƶ�����ָ�ԭʼλ��
            spriteTransform.localPosition = originalSpritePosition;
        }
    }
}
        if (Input.GetMouseButtonDown(0))
        {
            // ������������
            // ��ȡ�����λ�ã�ת��Ϊ��������
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // ȷ�� z ��Ϊ 0��2D ��Ϸ��

            // ��ʼ�ƶ�
            isMoving = true;
            elapsedTime = 0;
        }

        // �ƶ���ɫ
        if (isMoving)
        {
            // �����ƶ�����;���
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, targetPosition);

            // �����ֵλ��
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, elapsedTime / (distance / moveSpeed));
            newPosition.z = 0; // ȷ�� z ��Ϊ 0��2D ��Ϸ��

            // �ƶ���ɫ
            transform.position = newPosition;

            // �������ҡ��Ч��
            float sway = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
            spriteTransform.localPosition = originalSpritePosition + new Vector3(sway, 0, 0);

            // �����Ѿ�������ʱ��
            elapsedTime += Time.deltaTime;

            // ����Ŀ��λ�ã�ֹͣ�ƶ�
            if (elapsedTime >= (distance / moveSpeed))
            {
                isMoving = false;
                transform.position = targetPosition;
                spriteTransform.localPosition = originalSpritePosition;
            }

            // �߽���
            Vector3 currentPosition = transform.position;
            currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
            currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);
            transform.position = currentPosition;
        }

    }
}


using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public bool isMoving;
    public float swayAmount = 0.1f; // ҡ�ڵķ���
    public float swaySpeed = 5f;    // ҡ�ڵ��ٶ�
    public Transform spriteTransform; // ����������ҵ�SpriteTransform

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

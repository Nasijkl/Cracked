using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.z = -10; // �̶� z ֵΪ -10
        transform.position = targetPosition;
    }
}

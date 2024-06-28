using System.Collections;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    public float fogDisappearDelay = 1f; // ����ؿ���ʧ���ӳ�ʱ��

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            Debug.Log("Destroy fog tile: " + other.gameObject.name);
            StartCoroutine(DisappearFogTile(other.gameObject));
        }
    }

    private IEnumerator DisappearFogTile(GameObject fogTile)
    {
        // �ȴ�һ�����ӳ�ʱ��
        yield return new WaitForSeconds(fogDisappearDelay);

        // �ж�����ؿ��Ƿ��Ѿ�������
        if (fogTile != null)
        {
            // ��������ؿ�
            Destroy(fogTile);
        }
    }
}

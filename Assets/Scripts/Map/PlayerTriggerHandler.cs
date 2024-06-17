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
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            StartCoroutine(FadeOutFog(other.gameObject));
        }
    }

    IEnumerator FadeOutFog(GameObject fogObject)
    {
        SpriteRenderer renderer = fogObject.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            float fadeDuration = 1f; // ȷ��fadeDuration��InfiniteMapGenerator�е�ֵһ��
            Color color = renderer.color;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                renderer.color = color;
                yield return null;
            }

            color.a = 0f;
            renderer.color = color;
        }
    }*/
}

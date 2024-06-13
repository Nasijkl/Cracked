using System.Collections;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    public float fogDisappearDelay = 1f; // 迷雾地块消失的延迟时间

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
        // 等待一定的延迟时间
        yield return new WaitForSeconds(fogDisappearDelay);

        // 判断迷雾地块是否已经被销毁
        if (fogTile != null)
        {
            // 销毁迷雾地块
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
            float fadeDuration = 1f; // 确保fadeDuration与InfiniteMapGenerator中的值一致
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

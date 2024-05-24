using System.Collections;
using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
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
    }
}

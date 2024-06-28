using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    public GameObject fogTilePrefab; // ����ؿ��Prefab
    private List<GameObject> fogTiles; // ����ؿ��б�
    public float maxOffset = 0.5f;
    public int exclusionRange = 20;
    public float fogDisappearDuration = 1.0f; // ������ʧ�ĳ���ʱ��

    private void Start()
    {
        fogTiles = new List<GameObject>();

        // ��400*400���ط�Χ�ڸ�һ��������������ؿ�
        for (int x = -200; x < 200; x += 4)
        {
            for (int y = -200; y < 200; y += 4)
            {
                Vector2 position = new Vector2(x, y);
                if (Mathf.Abs(position.x) <= exclusionRange && Mathf.Abs(position.y) <= exclusionRange)
                {
                    continue; // ���ų���Χ�ڣ���������
                }
                float offsetX = Random.Range(-maxOffset, maxOffset);
                float offsetY = Random.Range(-maxOffset, maxOffset);
                position += new Vector2(offsetX, offsetY);
                GameObject fogTile = Instantiate(fogTilePrefab, position, Quaternion.identity);
                fogTile.tag = "Fog"; // ȷ������ؿ�ı�ǩ����Ϊ "Fog"
                fogTiles.Add(fogTile);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            // ��ȡ��ײ��������ؿ�
            GameObject collidedFogTile = other.gameObject;

            // ��ʼ������ʧЭ��
            StartCoroutine(FadeOutFogTile(collidedFogTile));
        }
    }

    private IEnumerator FadeOutFogTile(GameObject fogTile)
    {
        SpriteRenderer spriteRenderer = fogTile.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color originalColor = spriteRenderer.color;
            float elapsedTime = 0f;

            while (elapsedTime < fogDisappearDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fogDisappearDuration);
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null;
            }

            // ��ȫ͸������������ؿ�
            Destroy(fogTile);
        }
    }
}

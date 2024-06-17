using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    public GameObject fogTilePrefab; // 迷雾地块的Prefab
    private List<GameObject> fogTiles; // 迷雾地块列表
    public float maxOffset = 0.5f;
    public int exclusionRange = 20;
    public float fogDisappearDuration = 1.0f; // 渐变消失的持续时间

    private void Start()
    {
        fogTiles = new List<GameObject>();

        // 在400*400像素范围内隔一个像素生成迷雾地块
        for (int x = -200; x < 200; x += 4)
        {
            for (int y = -200; y < 200; y += 4)
            {
                Vector2 position = new Vector2(x, y);
                if (Mathf.Abs(position.x) <= exclusionRange && Mathf.Abs(position.y) <= exclusionRange)
                {
                    continue; // 在排除范围内，跳过生成
                }
                float offsetX = Random.Range(-maxOffset, maxOffset);
                float offsetY = Random.Range(-maxOffset, maxOffset);
                position += new Vector2(offsetX, offsetY);
                GameObject fogTile = Instantiate(fogTilePrefab, position, Quaternion.identity);
                fogTile.tag = "Fog"; // 确保迷雾地块的标签设置为 "Fog"
                fogTiles.Add(fogTile);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fog"))
        {
            // 获取碰撞到的迷雾地块
            GameObject collidedFogTile = other.gameObject;

            // 开始渐变消失协程
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

            // 完全透明后销毁迷雾地块
            Destroy(fogTile);
        }
    }
}

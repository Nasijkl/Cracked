using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    public GameObject fogTilePrefab; // 迷雾地块的Prefab
    //public int fogTileCount = 10000; // 迷雾地块数量
    //public float fogSpawnRadius = 1f; // 迷雾地块生成的半径
    //public float fogDisappearDelay = 0.1f; // 碰撞后迷雾地块消失的延迟时间

    private List<GameObject> fogTiles; // 迷雾地块列表
    public float maxOffset = 0.5f;
    public int exclusionRange = 20;

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
                fogTiles.Add(fogTile);
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取碰撞到的迷雾地块
            GameObject collidedFogTile = gameObject.transform.parent.gameObject;
            if (collidedFogTile.CompareTag("Fog"))
            {
                // 等待一定的延迟时间
                StartCoroutine(DisappearFogTile(collidedFogTile));
            }
        }
    }

    private IEnumerator DisappearFogTile(GameObject fogTile)
    {
        // 等待一定的延迟时间
        yield return new WaitForSeconds(fogDisappearDelay);

        // 销毁迷雾地块
        Destroy(fogTile);
    }*/

}

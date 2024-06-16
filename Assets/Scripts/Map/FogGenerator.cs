using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogGenerator : MonoBehaviour
{
    public GameObject fogTilePrefab; // ����ؿ��Prefab
    //public int fogTileCount = 10000; // ����ؿ�����
    //public float fogSpawnRadius = 1f; // ����ؿ����ɵİ뾶
    //public float fogDisappearDelay = 0.1f; // ��ײ������ؿ���ʧ���ӳ�ʱ��

    private List<GameObject> fogTiles; // ����ؿ��б�
    public float maxOffset = 0.5f;
    public int exclusionRange = 20;

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
                fogTiles.Add(fogTile);
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��ȡ��ײ��������ؿ�
            GameObject collidedFogTile = gameObject.transform.parent.gameObject;
            if (collidedFogTile.CompareTag("Fog"))
            {
                // �ȴ�һ�����ӳ�ʱ��
                StartCoroutine(DisappearFogTile(collidedFogTile));
            }
        }
    }

    private IEnumerator DisappearFogTile(GameObject fogTile)
    {
        // �ȴ�һ�����ӳ�ʱ��
        yield return new WaitForSeconds(fogDisappearDelay);

        // ��������ؿ�
        Destroy(fogTile);
    }*/

}

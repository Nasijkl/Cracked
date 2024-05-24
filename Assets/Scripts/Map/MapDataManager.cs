using UnityEngine;
using System.Collections.Generic;

public class MapDataManager : MonoBehaviour
{
    public static MapDataManager Instance { get; private set; }

    public Vector3 playerPosition;
    public Dictionary<Vector2, GameObject> mapTiles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 保持此对象在加载新场景时不会被销毁
            mapTiles = new Dictionary<Vector2, GameObject>();
        }
        else
        {
            Destroy(gameObject); // 确保只有一个实例存在
        }
    }

    public void SavePlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public void SaveMapTile(Vector2 position, GameObject tile)
    {
        if (!mapTiles.ContainsKey(position))
        {
            mapTiles.Add(position, tile);
        }
    }

    public void RestoreMapTiles(GameObject mapTilePrefab)
    {
        List<Vector2> keys = new List<Vector2>(mapTiles.Keys);

        foreach (var key in keys)
        {
            if (mapTiles[key] == null)
            {
                GameObject newTile = Instantiate(mapTilePrefab, new Vector3(key.x * 10, key.y * 10, 0), Quaternion.identity);
                mapTiles[key] = newTile;
            }
        }
    }
}
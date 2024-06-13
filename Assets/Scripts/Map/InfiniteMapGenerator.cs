using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    //public GameObject mapTilePrefab;
    public GameObject[] mapTilePrefabs; // an array to store all the mapTilePrefabs
    //public GameObject newMapTilePrefab; // 新的地图块Prefab
    public int seed;
    public int mapTileSize = 10;
    public int loadRadius = 2; // 减少加载半径，减少生成的地图块数量
    public float minDistanceBetweenTiles = 30f; // 增加地图块之间的最小距离
    public float maxRandomOffset = 10f; // 增加随机偏移的最大值
    public float fadeDuration = 1f; // 淡入效果持续时间
    public float stamina = 100f;
    public float staminaConsumptionRate = 1f;
    public float fatigueDamageRate = 1f;
    public float explorationValue = 0f;
    public float explorationThreshold = 100f;

    private Dictionary<Vector2, GameObject> mapTiles;
    private Vector2 playerCurrentTile;
    private GameObject player;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    void Start()
    {
        Random.InitState(seed);
        mapTiles = new Dictionary<Vector2, GameObject>();
        player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
        playerCurrentTile = GetTilePosition(player.transform.position);
        GenerateTilesAround(playerCurrentTile);
    }

    void Update()
    {
        Vector2 playerPosition = GetTilePosition(player.transform.position);

        if (playerPosition != playerCurrentTile)
        {
            playerCurrentTile = playerPosition;
            GenerateTilesAround(playerCurrentTile);
        }

        HandleStamina();
    }

    Vector2 GetTilePosition(Vector3 position)
    {
        return new Vector2(Mathf.Floor(position.x / mapTileSize), Mathf.Floor(position.y / mapTileSize));
    }

    void GenerateTilesAround(Vector2 centerTile)
    {
        for (int x = -loadRadius; x <= loadRadius; x++)
        {
            for (int y = -loadRadius; y <= loadRadius; y++)
            {
                Vector2 tilePosition = new Vector2(centerTile.x + x, centerTile.y + y);
                if (!mapTiles.ContainsKey(tilePosition))
                {
                    GenerateTile(tilePosition);
                }
            }
        }
    }

    void GenerateTile(Vector2 tilePosition)
    {
        Vector3 newPosition;
        int attempts = 0;
        do
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-maxRandomOffset, maxRandomOffset),
                Random.Range(-maxRandomOffset, maxRandomOffset),
                0
            );

            newPosition = new Vector3(tilePosition.x * mapTileSize, tilePosition.y * mapTileSize, 0) + randomOffset;
            attempts++;
        } while (!IsPositionValid(newPosition) && attempts < 10);

        if (attempts < 10) // 成功找到有效位置
        {
            //GameObject selectedPrefab = Random.value > 0.5f ? mapTilePrefab : newMapTilePrefab; // 随机选择一个地图块Prefab
            int randomIndex = Random.Range(0, mapTilePrefabs.Length); // generate a random index
            GameObject selectedPrefab = mapTilePrefabs[randomIndex]; // select a prefab from the array
            GameObject newTile = Instantiate(selectedPrefab, newPosition, Quaternion.identity);
            mapTiles[tilePosition] = newTile;
            StartCoroutine(FadeInFog(newTile));
            explorationValue += 10f; // Adjust as needed

            if (explorationValue >= explorationThreshold)
            {
                // Trigger Boss Battle
                TriggerBossBattle();
                explorationValue = 0f; // Reset exploration value after boss battle
            }
        }
    }

    IEnumerator FadeInFog(GameObject tile)
    {
        // 找到子物体中标签为 "Fog" 的SpriteRenderer
        Transform[] children = tile.GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("Fog"))
            {
                SpriteRenderer renderer = child.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    Color color = renderer.color;
                    color.a = 1f;
                    renderer.color = color;

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
    }

    bool IsPositionValid(Vector3 position)
    {
        foreach (var tile in mapTiles.Values)
        {
            if (Vector3.Distance(tile.transform.position, position) < minDistanceBetweenTiles)
            {
                return false;
            }
        }
        return true;
    }

    void HandleStamina()
    {
        if (playerMovement.isMoving)
        {
            stamina -= staminaConsumptionRate * Time.deltaTime;
            if (stamina <= 0)
            {
                stamina = 0;
                playerHealth.TakeDamage(fatigueDamageRate * Time.deltaTime);
            }
        }
    }

    void TriggerBossBattle()
    {
        // Implement Boss Battle Logic
        Debug.Log("Boss Battle Triggered!");
    }
}
